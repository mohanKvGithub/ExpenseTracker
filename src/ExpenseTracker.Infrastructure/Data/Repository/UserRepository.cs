using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Data.Repository;

public class UserRepository(ApplicationDbContext db):IUserRepository
{
    public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await db.User.Where(x => !x.IsDeleted && email.Equals(x.Email)).FirstOrDefaultAsync();
    }
}
