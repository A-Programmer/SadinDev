using KSProject.Application;
using KSProject.Domain;
using KSProject.Infrastructure;
using KSProject.Presentation;
using KSProject.WebApi;
using KSProject.WebApi.ExtensionMethods;
using Microsoft.AspNetCore.HttpOverrides;

var mainBuilder = WebApplication.CreateBuilder(args);

(WebApplicationBuilder builder,
	PublicSettings settings) = mainBuilder.AddBasicConfigurations();

builder.RegisterWebApi(builder.Configuration);
builder.Services.RegisterApplication();
builder.Services.RegisterPresentation(settings);
builder.Services.RegisterInfrastructure(builder.Configuration);
builder.Services.RegisterDomain();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
	ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UsePresentation();
app.UseDomain();
app.UseApplication();
app.UseInfrastructure();
app.UseWebApi();


app.Run();