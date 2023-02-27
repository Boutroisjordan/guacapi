using GuacAPI.Helpers;
using GuacAPI.Services.UserServices;
using Microsoft.Extensions.Options;

namespace GuacAPI.Authorization;
public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }
  

    public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
    {
        if(context.Request.Path.Value.Contains("RefreshToken"))
        {
            await _next(context);
            return;
        }
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
             var AccessToken = jwtUtils.ValidateToken(token);
            if (AccessToken != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetUserByRefreshToken(AccessToken);
            }
        }

        await _next(context);
    }
}