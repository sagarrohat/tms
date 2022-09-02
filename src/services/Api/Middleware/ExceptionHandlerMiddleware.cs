using System.Text.Json;
using Common;

namespace Api;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (AppException ex)
        {
            var errorResponse = HandleAppException(ex);
            await HandleErrorResponseAsync(httpContext, errorResponse);
        }
        catch (Exception ex)
        {
            var errorResponse = HandleException(ex);
            await HandleErrorResponseAsync(httpContext, errorResponse);
        }
    }

    private static async Task HandleErrorResponseAsync(HttpContext httpContext, ErrorResponse errorResponse)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = errorResponse.ErrorCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }

    private static ErrorResponse HandleAppException(AppException ex)
    {
        return new ErrorResponse(ex.ErrorCode, ex.Message);
    }

    private static ErrorResponse HandleException(Exception ex)
    {
        return new ErrorResponse(ErrorCodes.InternalError, ex.Message);
    }
}