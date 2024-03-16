using Dapper;
using Npgsql;

namespace Model.Infrastructure;

public class IndicatorRepository
{
    private readonly string _connectionString;

    public IndicatorRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Indicator>> GetIndicators()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT ""Id"" as ""IndicatorId"", ""Name"" as ""IndicatorName"" FROM ""Indicators"";";

        var result = await connection.QueryAsync<Indicator>(sql);

        return result.ToList();
    }
}
