using Dapper;
using Npgsql;

namespace Model.Infrastructure;

public class YearRepository
{
    private readonly string _connectionString;

    public YearRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Year>> GetYears()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT ""Id"" as ""YearId"", ""Name"" as ""YearName"" FROM ""Years"";";

        var result = await connection.QueryAsync<Year>(sql);

        return result.ToList();
    }
}
