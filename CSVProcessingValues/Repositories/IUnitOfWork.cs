namespace CSVProcessingValues.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}