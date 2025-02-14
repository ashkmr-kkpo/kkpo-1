using Npgsql;
using System.Threading.Tasks;
namespace Kkpo2.Services.Postgres;

public class PostgresStorage : IStorage
{
    private readonly NpgsqlDataSource dataSource;

    public PostgresStorage(NpgsqlDataSource dataSource)
    {
        this.dataSource = dataSource;
    }

    public async Task<int> InsertAsync(string tableName, Dictionary<string, object> data)
    {
        var columns = string.Join(", ", data.Keys);
        var values = string.Join(", ", data.Keys.Select(k => "@" + k));

        var commandText = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

        await using var connection = await this.dataSource.OpenConnectionAsync();
        await using var cmd = new NpgsqlCommand(commandText, connection);

        foreach (var kvp in data)
        {
            cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
        }

        return await cmd.ExecuteNonQueryAsync();
    }

    public async Task<int> UpdateAsync(string tableName, Dictionary<string, object> data, string whereClause)
    {
        var setClause = string.Join(", ", data.Keys.Select(k => $"{k} = @{k}"));

        var commandText = $"UPDATE {tableName} SET {setClause} WHERE {whereClause}";

        await using var connection = await this.dataSource.OpenConnectionAsync();
        await using var cmd = new NpgsqlCommand(commandText, connection);

        foreach (var kvp in data)
        {
            cmd.Parameters.AddWithValue(kvp.Key, kvp.Value);
        }

        return await cmd.ExecuteNonQueryAsync();
    }

    public async Task<int> DeleteAsync(string tableName, string whereClause)
    {
        var commandText = $"DELETE FROM {tableName} WHERE {whereClause}";

        await using var connection = await this.dataSource.OpenConnectionAsync();
        await using var cmd = new NpgsqlCommand(commandText, connection);

        return await cmd.ExecuteNonQueryAsync();
    }
}