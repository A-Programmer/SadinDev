using KSFramework.GenericRepository;
using KSProject.Domain.Contracts;
using KSProject.Infrastructure.BackgroundJobs;
using KSProject.Infrastructure.Data;
using KSProject.Infrastructure.Interceptors;
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

        builder.Services.AddDbContext<KSProjectDbContext>((sp, options) =>
        {
            var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

            options.UseNpgsql(builder.Configuration.GetConnectionString("KSProjectDbConnection"),
                    x => x.MigrationsAssembly("KSProject.Infrastructure")
                        .EnableRetryOnFailure(3))
                .AddInterceptors(interceptor)
                .EnableSensitiveDataLogging();
        });

        builder.EnrichNpgsqlDbContext<KSProjectDbContext>();
        
        Console.WriteLine($"\n\n\n\n\nConnection String: {configuration.GetConnectionString("KSProjectDbConnection")}\n\n\n\n\n\n");
        Console.WriteLine($"\n\n\n\n\n\nConnection String: {builder.Configuration.GetConnectionString("KSProjectDbConnection")}\n\n\n\n\n\n\n");

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
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IKSProjectUnitOfWork, KSProjectUnitOfWork>();
        return builder.Services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<KSProjectDbContext>();
        context.Database.Migrate();

        return app;
    }
}
