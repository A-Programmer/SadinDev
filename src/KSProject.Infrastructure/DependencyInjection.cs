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

namespace KSProject.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection RegisterInfrastructure(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

		services.AddDbContext<KSProjectDbContext>((sp, options) =>
		{
			var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

			options.UseSqlServer(configuration.GetConnectionString("Default"),
				x =>
					x.MigrationsAssembly("KSProject.Infrastructure"))
				.AddInterceptors(interceptor)
				.EnableSensitiveDataLogging();
		});

		services.AddQuartz(configure =>
		{
			var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

			configure
				.AddJob<ProcessOutboxMessagesJob>(jobKey)
				.AddTrigger(
					trigger =>
						trigger.ForJob(jobKey)
							.WithSimpleSchedule(
								schedule =>
									schedule.WithIntervalInMinutes(10)
										.RepeatForever()));

			configure.UseMicrosoftDependencyInjectionJobFactory();
		});

		services.AddQuartzHostedService();

		services.AddScoped<DbContext, KSProjectDbContext>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<IKSProjectUnitOfWork, KSProjectUnitOfWork>();
		return services;
	}

	public static WebApplication UseInfrastructure(this WebApplication app)
	{
		using var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
		var context = serviceScope.ServiceProvider.GetRequiredService<KSProjectDbContext>();
		context.Database.Migrate();

		return app;
	}
}