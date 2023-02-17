using System.Diagnostics;
using CSVProcessingValues.Contexts;
using CSVProcessingValues.Models;
using CSVProcessingValues.Tools;
using Microsoft.EntityFrameworkCore;

namespace CSVProcessingValues.Repositories;

public class ValueRepository : BaseRepository, IValueRepository
{
    public ValueRepository(AppDbContext context)
        : base(context)
    {
    }

    public async Task<IEnumerable<Value>> GetAll(ValueParameters valueParameters)
    {
        return await Context.Values.ToListAsync();
    }

    public async Task SaveAll(IEnumerable<Value> values)
    {
        Debug.Assert(Context.Values != null, "Context.Users != null");
        await Context.Values.AddRangeAsync();
    }
}