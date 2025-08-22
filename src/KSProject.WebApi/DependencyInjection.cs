using KSProject.Domain;

namespace KSProject.WebApi;

public static class DependencyInjection
{
	public static WebApplicationBuilder RegisterWebApi(this WebApplicationBuilder builder,
		IConfiguration configuration)
	{
		builder.Services.Configure<PublicSettings>(
			configuration.GetSection(nameof(PublicSettings)));

		return builder;
	}

	public static WebApplication UseWebApi(this WebApplication app)
	{
		return app;
	}
}