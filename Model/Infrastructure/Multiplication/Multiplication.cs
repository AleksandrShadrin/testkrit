namespace Model.Infrastructure;

public class Multiplication
{
    public string Name { get; set; } = default!;
    public string BaseMeasurementUnit { get; set; } = default!;
    public string CalculatedMeasurementUnit { get; set; } = default!;
    public int Power { get; set; }
};