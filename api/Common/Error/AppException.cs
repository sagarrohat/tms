using System.Runtime.Serialization;

namespace Common;

[Serializable]
public class AppException : Exception
{
    public int ErrorCode { get; private set; }

    public AppException(int errorCode, string errorMessage)
        : base(errorMessage)
    {
        ErrorCode = errorCode;
    }

    public AppException(int errorCode, string errorMessage, Exception innerException)
        : base(errorMessage, innerException)
    {
        ErrorCode = errorCode;
    }

    protected AppException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}

