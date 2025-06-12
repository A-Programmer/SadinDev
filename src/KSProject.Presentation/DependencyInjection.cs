using KSProject.Presentation.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Project.Presentation.ExtensionMethods;

namespace KSProject.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddGlobalExceptionHandling();
        services.AddCustomControllers();
        services.AddSwaggerGen();

        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseStatusCodePages();
        app.UseExceptionHandler();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
