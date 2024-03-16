using Dapper;
using Npgsql;

namespace Model.Infrastructure;

public class ConvertationRepository
{
    private readonly string _connectionString;

    public ConvertationRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Convertation>> GetResulting(TValue value)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT ""Name"", ""BaseMeasurementUnit"", ""ResultingMeasurementUnit"", ""Coefficient"" FROM ""Convertation""
                    WHERE ""BaseMeasurementUnit"" = @BaseUnit;";

        var result = await connection.QueryAsync<Convertation>(sql, new { BaseUnit = value.MeasurementUnit });

        return result.ToList();
    }
}