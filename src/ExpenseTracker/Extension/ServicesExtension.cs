using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.Services;
using TCW.Utility;

namespace ExpenseTracker.Extension;

public static class ServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<UtilsService>();
        return services;
    }
}
