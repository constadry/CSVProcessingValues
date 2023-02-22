using CSVProcessingValues.Models;

namespace CSVProcessingValues.Repositories.ResultRepository;

public interface IResultRepository
{
    Task<Result> SaveAsync(Result result);
    Task<List<Result>> GetAllAsync(ResultParameters parameters);
}