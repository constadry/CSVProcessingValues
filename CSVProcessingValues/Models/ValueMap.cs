using CsvHelper.Configuration;

namespace CSVProcessingValues.Models;

public sealed class ValueMap : ClassMap<Value>
{
    public ValueMap()
    {
        Map(v => v.StartDate).Index(0);
        Map(v => v.Time).Index(1);
        Map(v => v.Indication).Index(2);
    }
}