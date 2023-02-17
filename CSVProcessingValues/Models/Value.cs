namespace CSVProcessingValues.Models;

public class Value
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = new();
    public int Time { get; set; } = 0;
    public double Indication { get; set; } = 0;
}