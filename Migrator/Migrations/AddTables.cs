using FluentMigrator;
using FluentMigrator.Postgres;

namespace Migrator.Migrations;

[Migration(1)]
public sealed class AddTables : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("Indicators")
            .WithColumn("Id").AsInt32().Identity(PostgresGenerationType.Always).PrimaryKey()
            .WithColumn("Name").AsString().NotNullable();

        Create.Table("Resources")
            .WithColumn("Id").AsInt32().Identity(PostgresGenerationType.Always).PrimaryKey()
            .WithColumn("Name").AsString().NotNullable();

        Create.Table("Years")
            .WithColumn("Id").AsInt32().Identity(PostgresGenerationType.Always).PrimaryKey()
            .WithColumn("Name").AsString().NotNullable();

        Create.Table("Values")
            .WithColumn("Id").AsInt32().Identity(PostgresGenerationType.Always).PrimaryKey()
            .WithColumn("IndicatorId").AsInt32().ForeignKey("Indicators", "Id").NotNullable()
            .WithColumn("ResourceId").AsInt32().ForeignKey("Resources", "Id").NotNullable()
            .WithColumn("YearId").AsInt32().ForeignKey("Years", "Id").NotNullable()
            .WithColumn("MeasurementUnit").AsString().NotNullable()
            .WithColumn("Value").AsDouble();

        Create.Table("Multiplication")
            .WithColumn("Name").AsString().Unique()
            .WithColumn("BaseMeasurementUnit").AsString().NotNullable()
            .WithColumn("CalculatedMeasurementUnit").AsString().NotNullable()
            .WithColumn("Power").AsInt32().NotNullable();

        Create.PrimaryKey("PK_Base_Calculated")
            .OnTable("Multiplication").WithSchema("public")
            .Columns("BaseMeasurementUnit", "CalculatedMeasurementUnit");

        Create.Table("Convertation")
            .WithColumn("Name").AsString().Unique()
            .WithColumn("BaseMeasurementUnit").AsString().NotNullable()
            .WithColumn("ResultingMeasurementUnit").AsString().NotNullable()
            .WithColumn("Coefficient").AsDouble().NotNullable();

        Create.PrimaryKey("PK_Base_Resulting")
            .OnTable("Convertation").WithSchema("public")
            .Columns("BaseMeasurementUnit", "ResultingMeasurementUnit");
    }
}