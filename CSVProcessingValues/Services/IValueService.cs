using CSVProcessingValues.Communication;
using CSVProcessingValues.Models;

namespace CSVProcessingValues.Services;

public interface IValueService
{
    Task<IEnumerable<Value>> GetAll(ValueParameters valueParameters);  
    Task<ValueResponse> Get(string id);
    Task<ValueResponse> Save(Value value);
    Task<ValueResponse> Update(Value value);
    Task<ValueResponse> Delete(string id);
}