namespace CSVProcessingValues.Models;

public class Result
{
    public int Id { get; set; }
    public string FileName { get; set; }
    /// <summary>
    /// Все время (максимальное значение времени минус минимальное значение времени)
    /// </summary>
    public int Period { get; set; }

    /// <summary>
    /// •	Минимальное дата и время, как момент запуска первой операции
    /// </summary>
    public DateTime StartDate { get; set; }

    public double AverageTime { get; set; }
    
    public double AverageIndication { get; set; }
    
    public double MedianIndication { get; set; }
    
    public double MaxIndication { get; set; }
    
    public double MinIndication { get; set; }

    /// <summary>
    /// Количество строк во входящем файле csv
    /// </summary>
    public int ValuesCount { get; set; }
}