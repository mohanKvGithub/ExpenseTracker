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
    Task<List<Transaction>> GetTransactionsAsync(CancellationToken cancellationToken);
}
