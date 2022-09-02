namespace Api;

public class ErrorResponse
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    
    public ErrorResponse(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
}