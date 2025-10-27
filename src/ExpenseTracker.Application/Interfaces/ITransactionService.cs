using ExpenseTracker.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<int> AddTransactionAsync(TransactionDto transactionDto,CancellationToken cancellationToken);
        Task<List<TransactionDto>> GetTransactionsAsync(CancellationToken cancellationToken);
        Task<DashboardStatDto> GetStatAsync(CancellationToken cancellationToken);
    }
}
