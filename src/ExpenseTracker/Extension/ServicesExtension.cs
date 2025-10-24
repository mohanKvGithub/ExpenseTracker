using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Services;

namespace ExpenseTracker.Extension;

public static class ServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}
