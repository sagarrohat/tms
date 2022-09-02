namespace Common;

public static class UtcDateTimeExtensions
{
    private static DateTime AsUtc(this DateTime dateTime)
    {
        return dateTime.ToUniversalTime();
    }
    
    public static DateTime AsUtcDateOnly(this DateTime dateTime)
    {
        return dateTime.AsUtc().Date;
    }
}