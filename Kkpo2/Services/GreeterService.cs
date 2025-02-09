using Grpc.Core;
using Npgsql;

namespace Kkpo2.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> logger;
    private readonly NpgsqlDataSource dataSource;
    public GreeterService(ILogger<GreeterService> logger, NpgsqlDataSource dataSource)
    {
        this.logger = logger;
        this.dataSource = dataSource;
    }

    public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        await using var connection = await this.dataSource.OpenConnectionAsync();
        await using var cmd  = new NpgsqlCommand();
        cmd.Connection = connection;

        cmd.CommandText = @"INSERT INTO users (firstName) VALUES (@firstName)";
        cmd.Parameters.AddWithValue("firstName", request.Name);
        await cmd.ExecuteNonQueryAsync();
        return new HelloReply
        {
            Message = "Hello " + request.Name
        };
    }
}
