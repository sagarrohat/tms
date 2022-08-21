namespace Service;

public class ErrorResponse
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;

    public ErrorResponse(int errorCode)
    {
        ErrorCode = errorCode;
    }

    public ErrorResponse(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
}