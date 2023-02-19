using CSVProcessingValues.Extensions;
using CSVProcessingValues.Models;
using CSVProcessingValues.Repositories.ResultRepository;
using CSVProcessingValues.Tools;

namespace CSVProcessingValues.Services.ResultService;

public class ResultService : IResultService
{
    private List<Value> _values;
    private IResultRepository _resultRepository;

    public ResultService(List<Value> values, IResultRepository resultRepository)
    {
        _values = values;
        _resultRepository = resultRepository;
    }

    public void Execute(string fileName)
    {
        _resultRepository.SaveAsync(
            new Result
            {
                FileName = fileName,
                Period = GetAllTime(),
                StartDate = GetStartDateTime(),
                AverageTime = GetAverageTime(),
                AverageIndication = GetAverageIndicator(),
                MedianIndication = GetMedianIndicator(),
                ValuesCount = _values.Count,
            }
        );
    }

    public int GetAllTime()
    {
        var maxValue = _values.MaxBy(x => x.Time)?.Time ?? 0;
        var minValue = _values.MinBy(x => x.Time)?.Time ?? 0;

        return maxValue - minValue;
    }

    public DateTime GetStartDateTime()
    {
        return _values.MinBy(x => x.StartDate).StartDate;
    }
    
    public double GetAverageTime()
    {
        return _values.Average(x => x.Time);
    }
    
    public double GetAverageIndicator()
    {
        return _values.Average(x => x.Indication);
    }
    
    public double GetMedianIndicator()
    {
        return _values.Median(x => x.Indication).Value;
    }
}