// Changelogs Date  | Author                | Description
// 2023-12-23       | Anthony Coudène       | Creation

var builder = DistributedApplication.CreateBuilder(args);

const string databaseName = "biodiagnostics";

var mongoContainer = builder.AddMongoDB("mongo")
  .WithLifetime(ContainerLifetime.Persistent);

var mongoDatabase = mongoContainer.AddDatabase(databaseName);

var biodiagnosticsHost = builder.AddProject<Projects.BioDiagnostics_Host>("biodiagnostics-host")
  .WithReference(mongoDatabase)
  .WaitFor(mongoDatabase);

builder.AddProject<Projects.BioDiagnostics_WebApp>("biodiagnostics-webapp")
  .WaitFor(biodiagnosticsHost);

/// dotnet tool install -g aspire.cli --prerelease
/// aspire publish
builder.AddDockerComposePublisher();

builder.Build().Run();
