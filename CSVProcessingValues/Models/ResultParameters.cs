namespace CSVProcessingValues.Models;

public class ResultParameters
{
    public string FileName { get; set; } = string.Empty;

    public DateTime StartDate { get; set; } = DateTime.MinValue;

    public DateTime EndDate { get; set; } = DateTime.MaxValue;
    
    public double AverageIndicationMin { get; set; } = double.MinValue;

    public double AverageIndicationMax { get; set; } = double.MaxValue;
    
    public double AverageTimeMin { get; set; } = double.MinValue;
    
    public double AverageTimeMax { get; set; } = double.MaxValue;
}