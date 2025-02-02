using Grpc.Core;
using Npgsql;

namespace Kkpo2.Services;

public class KkpoUserService : KkpoUser.KkpoUserBase
{
    private readonly ILogger<GreeterService> logger;
    private readonly NpgsqlDataSource dataSource;
    public KkpoUserService(ILogger<GreeterService> logger, NpgsqlDataSource dataSource)
    {
        this.logger = logger;
        this.dataSource = dataSource;
    }

    public override async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request, ServerCallContext context)
    {
        await using var connection = await this.dataSource.OpenConnectionAsync();
        await using var cmd  = new NpgsqlCommand();
        cmd.Connection = connection;

        cmd.CommandText = @"INSERT INTO postgresdb (firstName) VALUES (@firstName)";
        cmd.Parameters.AddWithValue("firstName", request.Name);
        await cmd.ExecuteNonQueryAsync();
        return new RegisterUserResponse
        {
            Message = "Hello " + request.Name
        };
    }
}
