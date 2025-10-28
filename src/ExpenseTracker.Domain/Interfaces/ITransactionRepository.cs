using ExpenseTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Interfaces;

public interface ITransactionRepository
{
    Task<int> AddTransactionAysnc(Transaction transaction,CancellationToken cancellationToken);
    Task<bool> UpdateTransactionAsyn(Transaction transaction, CancellationToken cancellationToken);
    Task<List<Transaction>> GetTransactionsAsync(CancellationToken cancellationToken);
    Task<List<TransactionType>> GetTransactionTypesAsync(CancellationToken cancellationToken);
    Task<string> GetTransactionTypeById(int typeId, CancellationToken cancellationToken);
    Task<List<PaymentType>> GetAccountsAsync(CancellationToken cancellationToken);
    Task<string> GetAccountById(int typeId, CancellationToken cancellationToken);
}
