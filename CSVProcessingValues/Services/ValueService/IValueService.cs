using CSVProcessingValues.Communication;
using CSVProcessingValues.Models;

namespace CSVProcessingValues.Services.ValueService;

public interface IValueService
{
    Task<ValueResponse> GetAll(string fileName);
    Task<ValueResponse> SaveAll(string fileName, List<Value> values);
}