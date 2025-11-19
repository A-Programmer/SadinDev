using KSProject.Application.Contracts;
using KSProject.Domain;
using KSProject.Presentation.ExtensionMethods;
using KSProject.Presentation.Filters;
using KSProject.Presentation.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Routing;

namespace KSProject.Presentation;

public static class DependencyInjection
{
    const string corePolicyName = "ALLOWALL";
    public static IServiceCollection RegisterPresentation(this IServiceCollection services,
        PublicSettings settings)
    {

        services.AddCors(options =>
        {
            options.AddPolicy(corePolicyName, builder =>
            {
                builder
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod();
            });
        });
        services.AddUsageDebitFilter();
        services.AddScoped<IPermissionDiscoveryService, PermissionDiscoveryService>();
        services.AddEndpointsApiExplorer();
        services.AddGlobalExceptionHandling();
        services.AddCustomControllers();
        services.AddCustomAuthentication(settings.JwtOptions);
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "KSTemplate", Version = "v1" });

            // Add JWT Authentication support in Swagger
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

        });

        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors(corePolicyName);
        app.UseStatusCodePages();
        app.UseExceptionHandler();

        // app.UseHttpsRedirection();

        return app;
    }
}
