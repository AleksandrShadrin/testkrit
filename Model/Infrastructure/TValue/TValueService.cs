using Dapper;
using Npgsql;

namespace Model.Infrastructure;

public interface ITValueService
{
    Task<List<TValue>> GetTValues(Indicator indicator, Resource resource, Year year);
    Task<List<TValue>> GetTValues();
}

public class TValueService : ITValueService
{
    private readonly string _connectionString;

    public TValueService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<TValue>> GetTValues(Indicator indicator, Resource resource, Year year)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT v.""Id"" as ""ValueId"", v.""Value"" as ""Value"", v.""MeasurementUnit"" as ""MeasurementUnit""
                    FROM ""Values"" v
                    WHERE ""IndicatorId"" = @IndicatorId
                        and ""ResourceId"" = @ResourceId
                        and ""YearId"" = @YearId;";

        var result = await connection.QueryAsync<TValue>(
            sql,
            param: new { year.YearId, indicator.IndicatorId, resource.ResourceId });

        return result.Select(v =>
        {
            v.Year = year;
            v.Indicator = indicator;
            v.Resource = resource;

            return v;
        }).ToList();
    }

    public async Task<List<TValue>> GetTValues()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        var sql = @"SELECT v.""Id"" as ""ValueId"", v.""Value"" as ""Value"", v.""MeasurementUnit"" as ""MeasurementUnit"",
	            y.""Id"" as ""YearId"", y.""Name"" as ""YearName"",
	            i.""Id"" as ""IndicatorId"", i.""Name"" as ""IndicatorName"",
	            r.""Id"" as ""ResourceId"", r.""Name"" as ""ResourceName"" FROM ""Values"" v
            LEFT JOIN ""Indicators"" i ON v.""IndicatorId"" = i.""Id""
            LEFT JOIN ""Years"" y ON v.""YearId"" = y.""Id""
            LEFT JOIN ""Resources"" r ON r.""Id"" = v.""ResourceId"";";

        var result = await connection.QueryAsync<TValue, Year, Indicator, Resource, TValue>(
            sql,
            (value, year, indicator, resource) =>
            {
                value.Year = year;
                value.Indicator = indicator;
                value.Resource = resource;
                return value;
            },
            splitOn: "YearId, IndicatorId, ResourceId");

        return result.ToList();
    }
}