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

    public async Task<Value> Get(string id)
    {
        Debug.Assert(Context.Values != null, "Context.Users != null");
        return await Context.Values.FindAsync(id) ??
               throw new CustomException($"User not found by id {id}");
    }

    public async Task<Value> Save(Value entity)
    {
        Debug.Assert(Context.Values != null, "Context.Users != null");
        var result = await Context.Values.AddAsync(entity);
        return result.Entity;
    }

    public async Task<Value> Update(Value entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Value> Delete(Value entity)
    {
        Debug.Assert(Context.Values != null, "Context.Users != null");
        var result = Context.Values.Remove(entity);
        return result.Entity;
    }
}