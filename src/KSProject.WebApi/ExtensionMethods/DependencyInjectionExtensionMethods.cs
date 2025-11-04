using KSProject.Domain;

namespace KSProject.WebApi.ExtensionMethods;

public static class DependencyInjectionExtensionMethods
{
	public static (WebApplicationBuilder builder,
		PublicSettings _settings) AddBasicConfigurations(this WebApplicationBuilder builder)
	{
        var _settings = new PublicSettings();

        builder.Configuration.GetSection(nameof(PublicSettings)).Bind(_settings);

        return (builder, _settings);
	}

	public static WebApplication RegisterGeneralPipelines(this WebApplication app)
	{
		// app.UseHttpsRedirection();
		return app;
	}
}
