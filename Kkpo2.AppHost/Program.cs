var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Kkpo2>("kkpo2");
builder.Build().Run();
