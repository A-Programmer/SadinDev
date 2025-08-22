using KSProject.Domain;

namespace KSProject.WebApi.ExtensionMethods;

public static class DependencyInjectionExtensionMethods
{
	public static (WebApplicationBuilder builder,
		PublicSettings _settings) AddBasicConfigurations(this WebApplicationBuilder builder)
	{
		PublicSettings _settings = new();

		IConfiguration Configuration = builder.Environment.IsProduction()
			? new ConfigurationBuilder()
				.AddUserSecrets(Common.AssemblyReference.Assembly)
				.AddJsonFile("appsettings.json")
				.AddEnvironmentVariables()
				.Build()
			: new ConfigurationBuilder()
				.AddUserSecrets(Common.AssemblyReference.Assembly)
				.AddJsonFile("appsettings.Development.json")
				.Build();

		Configuration.GetSection(nameof(PublicSettings)).Bind(_settings);

		return (builder, _settings);
	}

	public static WebApplication RegisterGeneralPipelines(this WebApplication app)
	{
		// app.UseHttpsRedirection();
		return app;
	}
}
