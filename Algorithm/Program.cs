using Model.Infrastructure;

var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=secret;Database=postgres";

var valueService = new TValueService(connectionString);

var resourceRepository = new ResourceRepository(connectionString);
var yearRepository = new YearRepository(connectionString);
var indicatorRepository = new IndicatorRepository(connectionString);
var multiplicationRepository = new MultiplicationRepository(connectionString);
var convertationRepository = new ConvertationRepository(connectionString);

var indicators = await indicatorRepository.GetIndicators();
var resources = await resourceRepository.GetResources();
var years = await yearRepository.GetYears();

foreach (var indicator in indicators)
{
    foreach (var resource in resources)
    {
        foreach (var year in years)
        {
            var mValues = await valueService.GetTValues(indicator, resource, year);

            while (true)
            {
                var based = await mValues
                    .ToAsyncEnumerable()
                    .SelectAwait(async m => (MValue: m, Multiplications: await multiplicationRepository.GetBased(m)))
                    .Select(x =>
                    {
                        var b = x.Multiplications.Where(mn => mValues.All(m => m.MeasurementUnit != mn.BaseMeasurementUnit));
                        return (x.MValue, b.ToList());
                    })
                    .ToListAsync();

                based.ForEach(z => z.Item2.ForEach(b => mValues.Add(new TValue
                {
                    Indicator = indicator,
                    Resource = resource,
                    Year = year,
                    MeasurementUnit = b.BaseMeasurementUnit,
                    Value = z.MValue.Value * Math.Pow(10, -b.Power)
                })));

                var calculated = await mValues
                    .ToAsyncEnumerable()
                    .SelectAwait(async m => (MValue: m, Multiplications: await multiplicationRepository.GetCalculated(m)))
                    .Select(x =>
                    {
                        var b = x.Multiplications.Where(mn => mValues.All(m => m.MeasurementUnit != mn.CalculatedMeasurementUnit));
                        return (x.MValue, b.ToList());
                    })
                    .ToListAsync();

                calculated.ForEach(z => z.Item2.ForEach(b => mValues.Add(new TValue
                {
                    Indicator = indicator,
                    Resource = resource,
                    Year = year,
                    MeasurementUnit = b.CalculatedMeasurementUnit,
                    Value = z.MValue.Value * Math.Pow(10, b.Power)
                })));

                var resulting = await mValues
                    .ToAsyncEnumerable()
                    .SelectAwait(async m => (MValue: m, Multiplications: await convertationRepository.GetResulting(m)))
                    .Select(x =>
                    {
                        var b = x.Multiplications.Where(mn => mValues.All(m => m.MeasurementUnit != mn.ResultingMeasurementUnit));
                        return (x.MValue, b.ToList());
                    })
                    .ToListAsync();

                resulting.ForEach(z => z.Item2.ForEach(b => {
                    mValues.Add(new TValue
                    {
                        Indicator = indicator,
                        Resource = resource,
                        Year = year,
                        MeasurementUnit = b.ResultingMeasurementUnit,
                        Value = z.MValue.Value * b.Coefficient
                    });

                    if (b.Name.Contains("wh", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine(b.Name);
                        Console.WriteLine(mValues.Last().Value);
                    }
                }));

                if (based.All(b => b.Item2.Count == 0)
                    && calculated.All(b => b.Item2.Count == 0)
                    && resulting.All(b => b.Item2.Count == 0))
                {
                    break;
                }
            }
        }
    }
}