using CSVProcessingValues.Communication;
using CSVProcessingValues.Models;

namespace CSVProcessingValues.Services.ValueService;

public interface IValueService
{
    Task<IEnumerable<Value>> GetAll(ValueParameters valueParameters);
    Task<ValueResponse> SaveAll(string fileName, IEnumerable<Value> values);
}