using KSFramework.GenericRepository;
using KSFramework.Interceptors;
using KSProject.Domain.Contracts;
using KSProject.Infrastructure.BackgroundJobs;
using KSProject.Infrastructure.Data;
using KSProject.Infrastructure.ExtensionMethods;
using KSProject.Infrastructure.Helpers;
using KSProject.Infrastructure.Interceptors;
using KSProject.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Microsoft.Extensions.Hosting;

namespace KSProject.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IHostApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.Services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        builder.Services.AddSingleton<SoftDeleteInterceptor>();
        builder.Services.RegisterPaymentGateways(configuration);
        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddDbContext<KSProjectDbContext>((sp, options) =>
        {
            var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

            options.UseNpgsql(builder.Configuration.GetConnectionString("KSProjectDbConnection"),
                    x => x.MigrationsAssembly("KSProject.Infrastructure")
                        .EnableRetryOnFailure(3))
                .AddInterceptors(interceptor)
                .AddInterceptors(sp.GetRequiredService<SoftDeleteInterceptor>())
                .EnableSensitiveDataLogging();
        });

        builder.EnrichNpgsqlDbContext<KSProjectDbContext>();
        
        builder.Services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            configure
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(jobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule.WithIntervalInSeconds(10)
                                        .RepeatForever()));
        });

        builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        builder.Services.AddScoped<DbContext, KSProjectDbContext>();
        // builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IKSProjectUnitOfWork, KSProjectUnitOfWork>();
        return builder.Services;
    }

    public static WebApplication UseInfrastructureAsync(this WebApplication app)
    {
        using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<KSProjectDbContext>();
        context.Database.Migrate();
        
        app.UseApiKeyAuthentication();
        
        return app;
    }
}
