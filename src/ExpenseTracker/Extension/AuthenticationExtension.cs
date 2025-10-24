using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ExpenseTracker.Application.DTO;

namespace ExpenseTracker.Extension;

public static class AuthenticationExtension
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration, AppSettingDto appSettingDto)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Home/Login";
            options.LogoutPath = "/Home/Logout";
            options.Events.OnRedirectToLogin = ctx =>
            {
                if (!ctx.Request.Path.StartsWithSegments(new PathString("/api")))
                {
                    ctx.Response.Redirect(ctx.RedirectUri);
                    return Task.CompletedTask;
                }
                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return ctx.Response.WriteAsync("Unauthorized");
            };
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                AuthenticationType = JwtBearerDefaults.AuthenticationScheme,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettingDto.JWTKey)),
                ClockSkew = TimeSpan.Zero
            };
        });
        return services;
    }
}
