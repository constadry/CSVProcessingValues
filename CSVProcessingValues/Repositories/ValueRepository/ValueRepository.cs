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

    public async Task<IEnumerable<Value>> GetAll(ValueParameters valueParameters)
    {
        Debug.Assert(Context.Values != null, "Context.Values != null");
        return await Context.Values.ToListAsync();
    }

    public async Task SaveAll(IEnumerable<Value> values)
    {
        Debug.Assert(Context.Values != null, "Context.Values != null");
        await Context.Values.AddRangeAsync(values);
    }
}