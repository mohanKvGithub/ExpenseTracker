using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Infrastructure.Data;
using ExpenseTracker.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Extension;

public static class DataExtension
{
    public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                                        options.UseSqlServer(configuration.GetConnectionString("SqlConnection"),
                                        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
        return services;
    }
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        return services;
    }
}
