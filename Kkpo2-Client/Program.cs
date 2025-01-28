using Grpc.Net.Client;
using Kkpo2;

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
                  new HelloRequest { Name = "Anjali!" });
Console.WriteLine("Greeting: " + reply.Message);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();