using ExpenseTracker.Application.DTO;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Cryptography;
namespace ExpenseTracker.Application.Services;

public class UserService(IUserRepository userRepository,IHttpContextAccessor httpContextAccessor):IUserService
{
    public async Task<ServiceResponseDto<string>> AuthenticateUserAsync(UserDto userDto)
    {
        var user = await userRepository.GetUserByEmailAsync(userDto.Email, CancellationToken.None);

        if (user == null)
            return new ServiceResponseDto<string> { IsSuccess = false, Message = "User not found"};

        var hashedPassword = Convert.ToBase64String(SHA512.HashData(System.Text.Encoding.UTF8.GetBytes(userDto.Password + user.Salt)));
        if (hashedPassword != user.Password)
            return new ServiceResponseDto<string> { IsSuccess = false, Message = "Invalid Password"};

        await GenerateToken(user.Name,user.Id);
        return new ServiceResponseDto<string> { IsSuccess = true, Message = "Valid User"};
    }
    public async Task GenerateToken(string userName, int id)
    {
        var context= httpContextAccessor.HttpContext;
        var claims = new List<Claim>
            {
                new(ClaimTypes.Role, "Admin"),
                new(ClaimTypes.Name, userName),
                new("UserId",$"{id}"),
            };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties { IsPersistent = true });
    }
}
