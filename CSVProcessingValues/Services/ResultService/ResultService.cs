using CSVProcessingValues.Communication;
using CSVProcessingValues.Extensions;
using CSVProcessingValues.Models;
using CSVProcessingValues.Repositories.ResultRepository;
using CSVProcessingValues.Tools;

namespace CSVProcessingValues.Services.ResultService;

public class ResultService : IResultService
{
    private List<Value> _values;
    private IResultRepository _resultRepository;

    public ResultService(IResultRepository resultRepository)
    {
        _resultRepository = resultRepository;
    }

    public async Task<ResultResponse> ExecuteAsync(List<Value> values, string fileName)
    {
        try
        {
            var results = await _resultRepository.SaveAsync(GetResult(values, fileName));
            return new ResultResponse(new List<Result> {results});
        }
        catch (CustomException ex)
        {
            return new ResultResponse($"An error occurred when saving the user: {ex.Message}");
        }
    }

    public Result GetResult(List<Value> values, string fileName)
    {
        _values = values;
        return new Result
        {
            FileName = fileName,
            Period = GetAllTime(),
            StartDate = GetStartDateTime(),
            AverageTime = GetAverageTime(),
            AverageIndication = GetAverageIndicator(),
            MedianIndication = GetMedianIndicator(),
            MinIndication = _values.MinBy(x => x.Indication).Indication,
            MaxIndication = _values.MaxBy(x => x.Indication).Indication,
            ValuesCount = _values.Count,
        };
    }

    public int GetAllTime()
    {
        var maxValue = _values.MaxBy(x => x.Time)?.Time ?? 0;
        var minValue = _values.MinBy(x => x.Time)?.Time ?? 0;

        return maxValue - minValue;
    }

    public async Task<ResultResponse> GetAll(ResultParameters parameters)
    {
        try
        {
            var results = await _resultRepository.GetAllAsync(parameters);
            return new ResultResponse(results);
        }
        catch (CustomException ex)
        {
            return new ResultResponse($"An error occurred when saving the user: {ex.Message}");
        }
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