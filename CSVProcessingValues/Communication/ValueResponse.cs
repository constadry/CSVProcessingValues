using CSVProcessingValues.Models;

namespace CSVProcessingValues.Communication;

public class ValueResponse : BaseResponse
{
    private ValueResponse(bool success, string message, List<Value>? values)
        : base(success, message)
    {
        Values = values;
    }

    public ValueResponse(List<Value>? values) :
        this(true, string.Empty, values)
    {
    }

    public ValueResponse(string message) :
        this(false, message, null)
    {
    }

    //TODO: List
    public List<Value>? Values { get; }
}