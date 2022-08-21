﻿using Application;
using Microsoft.AspNetCore.Mvc;

namespace Service;

public static class LoginEndpoint
{
    public static async Task<JwtResponse> Login(HttpContext httpContext, [FromServices] ILoginCommand command,
        [FromBody] LoginRequest request)
    {
        return await command.ExecuteAsync(request);
    }
}