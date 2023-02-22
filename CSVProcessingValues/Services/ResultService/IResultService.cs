using CSVProcessingValues.Communication;
using CSVProcessingValues.Models;

namespace CSVProcessingValues.Services.ResultService;

public interface IResultService
{
    Task<ResultResponse> ExecuteAsync(List<Value> values, string fileName);
    int GetAllTime();
    public Task<ResultResponse> GetAll(ResultParameters parameters);
}