using CSVProcessingValues.Models;

namespace CSVProcessingValues.Repositories;

public interface IValueRepository
{
    Task<IEnumerable<Value>> GetAll(ValueParameters valueParameters);  
    Task SaveAll(IEnumerable<Value> values);
}