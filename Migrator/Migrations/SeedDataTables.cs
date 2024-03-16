using FluentMigrator;
namespace Migrator.Migrations;

[Migration(2)]
public sealed class SeedDataTables: ForwardOnlyMigration
{
    public override void Up()
    {
        Insert
            .IntoTable("Indicators")
            .Row(new { Name = "tfc" });

        Insert
            .IntoTable("Years")
            .Row(new { Name = "2017" })
            .Row(new { Name = "2018" })
            .Row(new { Name = "2019" })
            .Row(new { Name = "2020" })
            .Row(new { Name = "2021" });

        Insert
            .IntoTable("Resources")
            .Row(new { Name = "gas" });

        var multiplications = new List<object> {
            new { Name = "Gft3ng <--> ft3ng", CalculatedMeasurementUnit = "Gft3ng", Power = -9, BaseMeasurementUnit = "ft3ng" },
            new { Name = "Gtce <--> tce", CalculatedMeasurementUnit = "Gtce", Power = -9, BaseMeasurementUnit = "tce" },
            new { Name = "Gtoe <--> toe", CalculatedMeasurementUnit = "Gtoe", Power = -9, BaseMeasurementUnit = "toe" },
            new { Name = "MMbtu <--> btu", CalculatedMeasurementUnit = "MMbtu", Power = -6, BaseMeasurementUnit = "btu" },
            new { Name = "Mj <--> j", CalculatedMeasurementUnit = "Mj", Power = -6, BaseMeasurementUnit = "j" },
            new { Name = "Kboe <--> boe", CalculatedMeasurementUnit = "Kboe", Power = -3, BaseMeasurementUnit = "boe" },
            new { Name = "Mtoe <--> toe", CalculatedMeasurementUnit = "Mtoe", Power = -6, BaseMeasurementUnit = "toe" },
            new { Name = "Twh <--> wh", CalculatedMeasurementUnit = "Twh", Power = -12, BaseMeasurementUnit = "wh" },
            new { Name = "Ktoe <--> toe", CalculatedMeasurementUnit = "Ktoe", Power = -3, BaseMeasurementUnit = "toe" },
            new { Name = "Gj <--> j", CalculatedMeasurementUnit = "Gj", Power = -9, BaseMeasurementUnit = "j" },
            new { Name = "Mboe <--> boe", CalculatedMeasurementUnit = "Mboe", Power = -6, BaseMeasurementUnit = "boe" },
            new { Name = "Mtce <--> tce", CalculatedMeasurementUnit = "Mtce", Power = -6, BaseMeasurementUnit = "tce" },
            new { Name = "Gm3ng <--> m3ng", CalculatedMeasurementUnit = "Gm3ng", Power = -9, BaseMeasurementUnit = "m3ng" },
            new { Name = "Bboe <--> boe", CalculatedMeasurementUnit = "Bboe", Power = -9, BaseMeasurementUnit = "boe" },
            new { Name = "Qbtu <--> btu", CalculatedMeasurementUnit = "Qbtu", Power = -15, BaseMeasurementUnit = "btu" },
            new { Name = "Mm3ng <--> m3ng", CalculatedMeasurementUnit = "Mm3ng", Power = -6, BaseMeasurementUnit = "m3ng" },
            new { Name = "Mft3ng <--> ft3ng", CalculatedMeasurementUnit = "Mft3ng", Power = -6, BaseMeasurementUnit = "ft3ng" },
            new { Name = "Gwh <--> wh", CalculatedMeasurementUnit = "Gwh", Power = -9, BaseMeasurementUnit = "wh" },

        };

        var insertMultiplications = Insert.IntoTable("Multiplication");
        multiplications.ForEach(m => insertMultiplications.Row(m));

        var convertations = new List<object>
        {
            new { Name = "Mtce --> Mm3ng", BaseMeasurementUnit = "Mtce", Coefficient = 751.4768963, ResultingMeasurementUnit = "Mm3ng" },
            new { Name = "Gft3ng --> Twh", BaseMeasurementUnit = "Gft3ng", Coefficient = 0.301277062, ResultingMeasurementUnit = "Twh" },
            new { Name = "MMbtu --> Mj", BaseMeasurementUnit = "MMbtu", Coefficient = 1055.060005, ResultingMeasurementUnit = "Mj" },
            new { Name = "Bboe --> Qbtu", BaseMeasurementUnit = "Bboe", Coefficient = 0.58000001, ResultingMeasurementUnit = "Qbtu" },
            new { Name = "Gtoe --> Gtce", BaseMeasurementUnit = "Gtoe", Coefficient = 1.4285714, ResultingMeasurementUnit = "Gtce" },
            new { Name = "Gj --> Gwh", BaseMeasurementUnit = "Gj", Coefficient = 0.000277778, ResultingMeasurementUnit = "Gwh" },
            new { Name = "Ktoe --> Kboe", BaseMeasurementUnit = "Ktoe", Coefficient = 6.8419054, ResultingMeasurementUnit = "Kboe" },
            new { Name = "Gm3ng --> Gft3ng", BaseMeasurementUnit = "Gm3ng", Coefficient = 35.958043, ResultingMeasurementUnit = "Gft3ng" }
        };

        var insertConvertations = Insert.IntoTable("Convertation");
        convertations.ForEach(m => insertConvertations.Row(m));

        var values = new List<object>
        {
            new { IndicatorId = 1, ResourceId = 1, YearId = 1, MeasurementUnit = "Mtoe", Value = 148.67 },
            new { IndicatorId = 1, ResourceId = 1, YearId = 2, MeasurementUnit = "Mtoe", Value = 149.33 },
            new { IndicatorId = 1, ResourceId = 1, YearId = 3, MeasurementUnit = "Mtoe", Value = 150.00 },
            new { IndicatorId = 1, ResourceId = 1, YearId = 4, MeasurementUnit = "Mtoe", Value = 150.67 },
            new { IndicatorId = 1, ResourceId = 1, YearId = 5, MeasurementUnit = "Mtoe", Value = 151.33 },
        };

        var insertValues = Insert.IntoTable("Values");
        values.ForEach(m => insertValues.Row(m));
    }
}