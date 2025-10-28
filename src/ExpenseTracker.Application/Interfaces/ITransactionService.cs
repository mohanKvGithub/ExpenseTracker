using ExpenseTracker.Application.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Interfaces
{
    public interface ITransactionService
    {
        Task<ServiceResponseDto<int>> AddTransactionAsync(TransactionDto transactionDto,CancellationToken cancellationToken);
        Task<List<TransactionDto>> GetTransactionsAsync(CancellationToken cancellationToken);
        Task<DashboardStatDto> GetStatAsync(CancellationToken cancellationToken);
        Task<List<DropDownDto>> GetTransactionTypes(CancellationToken cancellationToken);
        Task<List<DropDownDto>> GetAccountsTypes(CancellationToken cancellationToken);
    }
}
