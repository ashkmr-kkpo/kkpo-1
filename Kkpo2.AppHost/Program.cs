using Grpc.Net.Client;
using GrpcService1;

//var builder = DistributedApplication.CreateBuilder(args);

//builder.AddProject<Projects.GrpcService1>("grpcservice1");

//builder.Build().Run();

var handler = new HttpClientHandler();
handler.ServerCertificateCustomValidationCallback =
    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

// The port number must match the port of the gRPC server.
var channel = GrpcChannel.ForAddress("https://localhost:7052", new GrpcChannelOptions()
{
    HttpHandler = handler
});
var client = new Greeter.GreeterClient(channel);
var reply = await client.SayHelloAsync(
                  new HelloRequest { Name = "Ashish!" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();