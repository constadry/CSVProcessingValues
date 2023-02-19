namespace CSVProcessingValues.Models;

public class Value
{
    public int Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public int Time { get; set; }
    public double Indication { get; set; }
}