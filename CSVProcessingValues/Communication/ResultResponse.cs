using CSVProcessingValues.Models;

namespace CSVProcessingValues.Communication;

public class ResultResponse : BaseResponse
{
    private ResultResponse(bool success, string message, List<Result>? results)
        : base(success, message)
    {
        Values = results;
    }

    public ResultResponse(List<Result>? results) :
        this(true, string.Empty, results)
    {
    }

    public ResultResponse(string message) :
        this(false, message, null)
    {
    }
    
    public List<Result>? Values { get; }
}