using Dapper;
using Npgsql;

namespace Model.Infrastructure;

public class MultiplicationRepository
{
    private readonly string _connectionString;

    public MultiplicationRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Multiplication>> GetCalculated(TValue value)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT ""Name"", ""BaseMeasurementUnit"", ""CalculatedMeasurementUnit"", ""Power"" FROM ""Multiplication""
                    WHERE ""BaseMeasurementUnit"" = @BaseUnit;";

        var result = await connection.QueryAsync<Multiplication>(sql, new { BaseUnit = value.MeasurementUnit });

        return result.ToList();
    }

    public async Task<List<Multiplication>> GetBased(TValue value)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT ""Name"", ""BaseMeasurementUnit"", ""CalculatedMeasurementUnit"", ""Power"" FROM ""Multiplication""
                    WHERE ""CalculatedMeasurementUnit"" = @BaseUnit;";

        var result = await connection.QueryAsync<Multiplication>(sql, new { BaseUnit = value.MeasurementUnit });

        return result.ToList();
    }
}