using CSVProcessingValues.Models;

namespace CSVProcessingValues.Services.ResultService;

public interface IResultService
{
    Task<Result> ExecuteAsync(List<Value> values, string fileName);
    int GetAllTime();
}