using CSVProcessingValues.Models;

namespace CSVProcessingValues.Services.ResultService;

public class ResultService : IResultService
{
    private List<Value> _values;

    public ResultService(List<Value> values)
    {
        _values = values;
    }


    public void Execute()
    {
        
    }

    public int GetAllTime()
    {
        throw new NotImplementedException();
    }
}