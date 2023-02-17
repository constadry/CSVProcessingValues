using CSVProcessingValues.Models;

namespace CSVProcessingValues.Repositories;

public interface IValueRepository
{
    Task<IEnumerable<Value>> GetAll(ValueParameters valueParameters);  
    Task<Value> Get(string id);
    Task<Value> Save(Value entity);
    Task<Value> Update(Value entity);
    Task<Value> Delete(Value entity);
}