﻿namespace Common;

public static class ErrorCodes
{
    public const int BadRequest = 400;
    public const int Unauthorized = 401;
    public const int Forbidden = 403;
    public const int NotFound = 404;
    public const int Conflict = 409;
    public const int UnprocessableEntity = 422;
    public const int TooManyRequests = 429;
    public const int InternalError = 500;
}
