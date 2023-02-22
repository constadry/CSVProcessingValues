using System.Diagnostics;
using CSVProcessingValues.Contexts;
using CSVProcessingValues.Models;
using Microsoft.EntityFrameworkCore;

namespace CSVProcessingValues.Repositories.ValueRepository;

public class ValueRepository : BaseRepository, IValueRepository
{
    public ValueRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<List<Value>> GetAll(string fileName)
    {
        Debug.Assert(Context.Values != null, "Context.Values != null");
        return await Context.Values.Where(x => x.FileName == fileName).ToListAsync();
    }

    public async Task SaveAll(IEnumerable<Value> values)
    {
        Debug.Assert(Context.Values != null, "Context.Values != null");
        var existFileValues = Context
            .Values
            .Where(
                x => x
                    .FileName == values.FirstOrDefault()
                    .FileName);
        Context.Values.RemoveRange(existFileValues);
        await Context.Values.AddRangeAsync(values);
    }
}