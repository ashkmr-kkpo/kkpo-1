using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Npgsql;
using System.Threading;
using Kkpo2.Services.Postgres;

namespace Kkpo2Test.Postgres.Tests;
[TestClass]
public class PostgresStorageTests
{
    private readonly Mock<NpgsqlDataSource> mockDataSource;
    private readonly Mock<NpgsqlConnection> mockConnection;
    private readonly Mock<NpgsqlCommand> mockCommand;
    private readonly PostgresStorage storage;

    public PostgresStorageTests()
    {
        mockDataSource = new Mock<NpgsqlDataSource>();
        mockConnection = new Mock<NpgsqlConnection>();
        mockCommand = new Mock<NpgsqlCommand>();

        mockDataSource.Setup(ds => ds.OpenConnectionAsync(new CancellationToken())).ReturnsAsync(mockConnection.Object);
        mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);

        storage = new PostgresStorage(mockDataSource.Object);
    }

    [TestMethod]
    public async Task InsertAsync_ShouldExecuteNonQuery()
    {
        // Arrange
        var tableName = "users";
        var data = new Dictionary<string, object> { { "firstName", "John" } };
        mockCommand.Setup(c => c.ExecuteNonQueryAsync()).ReturnsAsync(1);

        // Act
        var result = await storage.InsertAsync(tableName, data);

        // Assert
        Assert.AreEqual(1, result);
        mockCommand.Verify(c => c.ExecuteNonQueryAsync(), Times.Once);
    }

    [TestMethod]
    public async Task UpdateAsync_ShouldExecuteNonQuery()
    {
        // Arrange
        var tableName = "users";
        var data = new Dictionary<string, object> { { "firstName", "Jane" } };
        var whereClause = "id = 1";
        mockCommand.Setup(c => c.ExecuteNonQueryAsync()).ReturnsAsync(1);

        // Act
        var result = await storage.UpdateAsync(tableName, data, whereClause);

        // Assert
        Assert.AreEqual(1, result);
        mockCommand.Verify(c => c.ExecuteNonQueryAsync(), Times.Once);
    }

    [TestMethod]
    public async Task DeleteAsync_ShouldExecuteNonQuery()
    {
        // Arrange
        var tableName = "users";
        var whereClause = "id = 1";
        mockCommand.Setup(c => c.ExecuteNonQueryAsync()).ReturnsAsync(1);

        // Act
        var result = await storage.DeleteAsync(tableName, whereClause);

        // Assert
        Assert.AreEqual(1, result);
        mockCommand.Verify(c => c.ExecuteNonQueryAsync(), Times.Once);
    }
}
