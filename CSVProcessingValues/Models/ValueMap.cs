using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

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

public class DateConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (text is null) return null;
        var time = text.Split('_').LastOrDefault() ?? string.Empty;
        var date = text.Replace($"_{time}", "");
        time = time.Replace("-", ":");
        var normalaizedString = date + " " + time;
        return DateTime.Parse(normalaizedString);
    }
}