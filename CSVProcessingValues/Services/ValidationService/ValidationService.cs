using CSVProcessingValues.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVProcessingValues.Services.ValidationService;

public class ValidationService : IValidationService
{
    public Task<List<Result>> FilterResultsAsync(DbSet<Result> results, ResultParameters parameters)
    {
        return results.Where(
            x => (x.StartDate >= parameters.StartDate || x.StartDate <= parameters.EndDate)
                 && (x.AverageTime >= parameters.AverageTimeMin || x.AverageTime <= parameters.AverageTimeMax)
                 && (x.AverageIndication >= parameters.AverageIndicationMin || x.AverageIndication <= parameters.AverageIndicationMax)
        ).ToListAsync();
    }
}