namespace Model.Infrastructure;

public class TValue
{
    public int Id { get; set; }
    public string MeasurementUnit { get; set; } = default!;
    public double Value { get; set; }
    public Indicator Indicator { get; set; } = default!;
    public Resource Resource { get; set; } = default!;
    public Year Year { get; set; } = default!;
}