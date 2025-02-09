using Npgsql;

public interface IStorage
{
    Task<int> InsertAsync(string tableName, Dictionary<string, object> data);
    Task<int> UpdateAsync(string tableName, Dictionary<string, object> data, string whereClause);
    Task<int> DeleteAsync(string tableName, string whereClause);
}