using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgresPassword = builder.AddParameter("postgres-password", "Aa@123456", secret: true);

// .AddPostgres("This is the name of the server in adminer login page",....)
var postgres = builder.AddPostgres("ksproject-db", password: postgresPassword)
    .WithImage("postgres:latest")
    .WithEnvironment("POSTGRES_USER", "postgres")
    .WithEnvironment("POSTGRES_PASSWORD", postgresPassword.Resource.Value)
    .WithDataVolume("db_data")
    .AddDatabase("KSProjectDbConnection", "KSProjectDb");

var adminer = builder.AddContainer("adminer", "adminer:latest")
    .WithHttpEndpoint(port: 8080, targetPort: 8080)
    .WaitFor(postgres);

adminer.WithReference(postgres);

var webApi = builder.AddProject<KSProject_WebApi>("webapi")
    .WithReference(postgres)
    .WaitFor(postgres);

builder.Build().Run();
