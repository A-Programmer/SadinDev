using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var webApi = builder.AddProject<KSProject_WebApi>("webapi");

builder.Build().Run();
