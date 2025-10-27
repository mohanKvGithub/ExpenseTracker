using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.DTO;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Interfaces;
using System.Net.Http.Headers;

namespace ExpenseTracker.Application.Services
{
    public class TransactionService(ITransactionTypeRepository transactionTypeRepository,ITransactionRepository transactionRepository):ITransactionService
    {
        public async Task<ServiceResponseDto<int>> AddTransactionAsync(TransactionDto transactionDto, CancellationToken cancellationToken)
        {
            var transactionType = await transactionTypeRepository.GetTransactionTypeById(transactionDto.TransactionTypeId,cancellationToken);
            var transaction = new Transaction
            {
                TransactionTypeId=transactionDto.TransactionTypeId,
                PaymentTypeId=transactionDto.PaymentTypeId,
                Cr=transactionType=="Income"?transactionDto.Amount:0,
                Dr=transactionType=="Expense"?transactionDto.Amount:0,
            };
        }
        public async Task<ServiceResponseDto<List<TransactionDto>>> GetTransactionsAsync(CancellationToken cancellationToken)
        {
            var transactions =await transactionRepository.GetTransactionsAsync(cancellationToken);
            var data= transactions.Select(x=>new TransactionDto
            {
                Id=x.Id,
                TransactionTypeId=x.tran,
                PaymentTypeId=x.PaymentTypeId,
                Amount=x.Cr>0?x.Cr:x.Dr,
            }).ToList();

        }
        public Task<ServiceResponseDto<DashboardStatDto>> GetStatAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
