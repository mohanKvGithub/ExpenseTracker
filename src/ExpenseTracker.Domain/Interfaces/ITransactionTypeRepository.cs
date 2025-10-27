using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Domain.Interfaces
{
    public interface ITransactionTypeRepository
    {
        Task<List<TransactionType>> GetTransactionTypesAsync(string typeName, CancellationToken cancellationToken);
        Task<string> GetTransactionTypeById(int typeId, CancellationToken cancellationToken);
    }
}
