
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Application.DTO;
namespace ExpenseTracker.Infrastructure.Data.Repository;

public class TransactionRepository(ApplicationDbContext db) : ITransactionRepository
{
    public async Task<int> AddTransactionAysnc(Transaction transaction, CancellationToken cancellationToken)
    {
        await db.Transaction.AddAsync(transaction, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        return transaction.Id;
    }
    public async Task<List<Transaction>> GetTransactionsAsync(CancellationToken cancellationToken)
    {
        return await db.Transaction
            .Where(x=>!x.IsDeleted)
            .Include(x=>x.TransactionType)
            .Include(x=>x.PaymentType)
            .ToListAsync(cancellationToken);
    }
    public async Task<bool> UpdateTransactionAsyn(Transaction transaction, CancellationToken cancellationToken)
    {
        db.Transaction.Update(transaction);
        await db.SaveChangesAsync(cancellationToken);
        return true; 
    }
}
