var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgWeb()
    //.WithDataVolume("postgres-data")
    .WithInitBindMount(@"C:\Users\ashis\source\repos\Kkpo2\Kkpo2.AppHost\db-init");
var postgresdb = postgres.AddDatabase("postgresdb2");
builder.AddProject<Projects.Kkpo2>("kkpo2").WithReference(postgresdb);

builder.Build().Run();
