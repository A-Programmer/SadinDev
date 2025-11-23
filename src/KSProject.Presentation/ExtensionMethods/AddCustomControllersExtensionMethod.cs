using System.Net.Mime;
using System.Text.Json.Serialization;
using KSProject.Presentation.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace KSProject.Presentation.ExtensionMethods;

public static class AddCustomControllersExtensionMethod
{
    public static IServiceCollection AddCustomControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
            {
                options.Filters.Add(typeof(UsageDebitFilter));
            })
            .AddApplicationPart(Application.AssemblyReference.Assembly)
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

        return services;
    }
}
