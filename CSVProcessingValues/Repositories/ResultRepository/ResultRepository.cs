using System.Diagnostics;
using CSVProcessingValues.Contexts;
using CSVProcessingValues.Models;
using CSVProcessingValues.Services.ValidationService;
using Microsoft.EntityFrameworkCore;

namespace CSVProcessingValues.Repositories.ResultRepository;

public class ResultRepository : BaseRepository, IResultRepository
{
    private IValidationService _validationService;
    public ResultRepository(AppDbContext context, IValidationService validationService)
        : base(context)
    {
        _validationService = validationService;
    }
    
    public async Task<Result> SaveAsync(Result result)
    {
        Debug.Assert(Context.Results != null, "Context.Results != null");
        var existFileResult = await Context.Results.SingleOrDefaultAsync(x => x.FileName == result.FileName);
        if (existFileResult is null)
        {
            var response = await Context.Results.AddAsync(result);
            return response.Entity;
        }
        else
        {
            existFileResult.Period = result.Period;
            existFileResult.AverageIndication = result.AverageIndication;
            existFileResult.AverageTime = result.AverageTime;
            existFileResult.MaxIndication = result.MaxIndication;
            existFileResult.MinIndication = result.MinIndication;
            existFileResult.StartDate = result.StartDate;
            existFileResult.ValuesCount = result.ValuesCount;
            return existFileResult;
        }
    }

    public async Task<List<Result>> GetAllAsync(ResultParameters parameters)
    {
        Debug.Assert(Context.Results != null, "Context.Results != null");
        return await _validationService.FilterResultsAsync(Context.Results, parameters);
    }
}