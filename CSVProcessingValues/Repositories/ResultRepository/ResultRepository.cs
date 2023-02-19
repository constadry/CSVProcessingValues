using System.Diagnostics;
using CSVProcessingValues.Contexts;
using CSVProcessingValues.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVProcessingValues.Repositories.ResultRepository;

public class ResultRepository : BaseRepository, IResultRepository 
{
    public ResultRepository(AppDbContext context)
        : base(context)
    {
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
            result.Id = existFileResult.Id;
            var response = Context.Results.Update(result);
            return response.Entity;
        }
    }

    public async Task<List<Result>> GetAllAsync(string fileName)
    {
        Debug.Assert(Context.Results != null, "Context.Results != null");
        return await Context.Results.Where(x => x.FileName == fileName).ToListAsync();
    }
}