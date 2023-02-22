using CSVProcessingValues.Models;

namespace CSVProcessingValues.Repositories.ValueRepository;

public interface IValueRepository
{
    Task<List<Value>> GetAll(string fileName);  
    Task SaveAll(IEnumerable<Value> values);
}