using CSVProcessingValues.Contexts;

namespace CSVProcessingValues.Repositories;

public abstract class BaseRepository
{
    protected readonly AppDbContext Context;

    protected BaseRepository(AppDbContext context)
    {
        Context = context;
    }
}