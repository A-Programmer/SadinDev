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

builder.AddServiceDefaults();
builder.Services.AddHttpContextAccessor();
builder.RegisterWebApi(builder.Configuration);
builder.Services.RegisterApplication();
builder.Services.RegisterPresentation(settings);
builder.RegisterInfrastructure(builder.Configuration);
builder.Services.RegisterDomain();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
	ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UsePresentation();
app.UseDomain();
app.UseApplication();
app.UseInfrastructureAsync();
app.UseWebApi();


app.Run();
