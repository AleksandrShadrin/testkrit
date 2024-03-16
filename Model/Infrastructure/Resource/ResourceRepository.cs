using Dapper;
using Npgsql;

namespace Model.Infrastructure;

public class ResourceRepository
{
    private readonly string _connectionString;

    public ResourceRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Resource>> GetResources()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT ""Id"" as ""ResourceId"", ""Name"" as ""ResourceName"" FROM ""Resources"";";

        var result = await connection.QueryAsync<Resource>(sql);

        return result.ToList();
    }
}
