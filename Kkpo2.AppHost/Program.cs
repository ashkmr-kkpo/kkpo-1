var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgWeb()
    .WithDataVolume("postgres-data");
var postgresdb = postgres.AddDatabase("postgresdb");
builder.AddProject<Projects.Kkpo2>("kkpo2").WithReference(postgresdb);

builder.Build().Run();
