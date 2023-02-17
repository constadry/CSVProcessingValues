namespace CSVProcessingValues.Models;

public class ValueParameters
{
    private int _pageSize = 13;
    private const int MaxPageSize = 13;
    private const int MinAge = 0;
    private const int MaxAge = 150;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    public string? Sex { get; set; }

    public int StartAge { get; set; } = MinAge;
    public int EndAge { get; set; } = MaxAge;
}