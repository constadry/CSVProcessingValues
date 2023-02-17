namespace CSVProcessingValues.Tools;

public class CustomException : Exception
{
    public CustomException() { }
    public CustomException(string message) : base(message) { }
}