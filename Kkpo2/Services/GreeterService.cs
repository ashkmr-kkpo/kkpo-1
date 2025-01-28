using Grpc.Core;
using Kkpo2;
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
        var create = @"
            CREATE TABLE users(
                id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
                name VARCHAR(255) NOT NULL,
            )";
        cmd.CommandText = create;
        await cmd.ExecuteNonQueryAsync();

        cmd.CommandText = @"INSERT INTO users (name) VALUES (@name)";
        cmd.Parameters.AddWithValue("name", request.Name);

        return new HelloReply
        {
            Message = "Hello " + request.Name
        };
    }
}
