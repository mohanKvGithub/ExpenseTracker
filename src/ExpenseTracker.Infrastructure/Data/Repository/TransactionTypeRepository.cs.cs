using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Data.Repository
{
    public class TransactionTypeRepository(ApplicationDbContext db): ITransactionTypeRepository 
    {
        public async Task<List<TransactionType>> GetTransactionTypesAsync(string typeName, CancellationToken cancellationToken)
        {
           return await db.TransactionType.Where(x => !x.IsDeleted)
                .ToListAsync(cancellationToken);
        }
        public async Task<string> GetTransactionTypeById(int typeId, CancellationToken cancellationToken)
        {
            var type = await db.TransactionType
                 .Where(x => !x.IsDeleted && x.Id == typeId)
                 .Select(x => x.Type)
                 .FirstOrDefaultAsync(cancellationToken);
            return type??"";
        }
    }
}
