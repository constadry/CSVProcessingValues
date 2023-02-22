using CSVProcessingValues.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVProcessingValues.Services.ValidationService;

public interface IValidationService
{
    Task<List<Result>> FilterResultsAsync(DbSet<Result> results, ResultParameters parameters);
}