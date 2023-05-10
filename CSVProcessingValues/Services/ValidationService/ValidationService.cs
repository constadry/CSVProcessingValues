using CSVProcessingValues.Models;
using CSVProcessingValues.Tools;
using Microsoft.EntityFrameworkCore;

namespace CSVProcessingValues.Services.ValidationService;

public class ValidationService : IValidationService
{
    public Task<List<Result>> FilterResultsAsync(DbSet<Result> results, ResultParameters parameters)
    {
        return results.Where(
            x => (x.StartDate >= parameters.StartDate || x.StartDate <= parameters.EndDate)
                 && (x.AverageTime >= parameters.AverageTimeMin || x.AverageTime <= parameters.AverageTimeMax)
                 && (x.AverageIndication >= parameters.AverageIndicationMin ||
                     x.AverageIndication <= parameters.AverageIndicationMax)
        ).ToListAsync();
    }

    public void CheckValidValues(List<Value> values)
    {
        if (values.Any(x =>
                x.Indication < 0
                || x.Time <= 0
                || x.StartDate >= DateTime.Now
                || x.StartDate <= DateTime.Parse("01.01.2000"))
            || values.Count > 10000)
            throw new CustomException("Not valid value in file");
    }
}