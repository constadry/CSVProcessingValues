using CSVProcessingValues.Models;

namespace CSVProcessingValues.Communication;

public class ValueResponse : BaseResponse
{
    private ValueResponse(bool success, string message, Value? value)
        : base(success, message)
    {
        Value = value;
    }

    public ValueResponse(Value? value) :
        this(true, string.Empty, value)
    {
    }

    public ValueResponse(string message) :
        this(false, message, null)
    {
    }

    public Value? Value { get; }
}