using KSFramework.KSDomain;
using KSFramework.KSMessaging.Abstraction;
using KSFramework.KSMessaging.Extensions;
using KSProject.Application.Services;
using KSProject.Domain.Aggregates.Users.Events;
using KSProject.Domain.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace KSProject.Application;

public static class DependencyInjection
{
	public static IServiceCollection RegisterApplication(this IServiceCollection services)
	{
		services.AddScoped<IJwtService, JwtService>();
		services.AddKSFramework(AssemblyReference.Assembly);
		return services;
	}

	public static WebApplication UseApplication(this WebApplication app)
	{
		return app;
	}
}
