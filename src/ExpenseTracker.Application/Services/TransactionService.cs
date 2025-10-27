using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.DTO;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Interfaces;

namespace ExpenseTracker.Application.Services
{
    public class TransactionService(ITransactionTypeRepository transactionTypeRepository,ITransactionRepository transactionRepository):ITransactionService
    {
        public async Task<int> AddTransactionAsync(TransactionDto transactionDto, CancellationToken cancellationToken)
        {
            var transactionType = await transactionTypeRepository.GetTransactionTypeById(transactionDto.TransactionTypeId,cancellationToken);
            var transaction = new Transaction
            {
                TransactionTypeId=transactionDto.TransactionTypeId,
                PaymentTypeId=transactionDto.PaymentTypeId,
                Cr=transactionType=="Income"?transactionDto.Amount:0,
                Dr=transactionType=="Expense"?transactionDto.Amount:0,
                Description=transactionDto.Description,
                TransactedOn=DateTime.UtcNow,
                CreatedOn=DateTime.UtcNow,
                UpdatedOn=DateTime.UtcNow,
                IsDeleted=false,
            };
           return await transactionRepository.AddTransactionAysnc(transaction,cancellationToken);
        }
        public async Task<List<TransactionDto>> GetTransactionsAsync(CancellationToken cancellationToken)
        {
            var transactions =await transactionRepository.GetTransactionsAsync(cancellationToken);
            return transactions.Select(x=>new TransactionDto
            {
                Id=x.Id,
                TransactionType=x.TransactionType.Type,
                PaymentTypeId=x.PaymentTypeId,
                Amount=x.Cr>0?x.Cr:x.Dr,
                Description=x.Description,
                TransactedOn=x.TransactedOn,
                PaymentType=x.PaymentType.AccountName,
            }).ToList();
        }
        public async Task<DashboardStatDto> GetStatAsync(CancellationToken cancellationToken)
        {
            var transactions =await transactionRepository.GetTransactionsAsync(cancellationToken);
            decimal currentBalance = 0;
            decimal totalIncome = 0;
            decimal totalExpense = 0;
            decimal budgetAmount = 0;
            foreach (var transaction in transactions)
            {
                currentBalance += transaction.Cr - transaction.Dr;
                totalIncome += transaction.Cr;
                totalExpense += transaction.Dr;
            }
            return new DashboardStatDto
            {
                CurrentBalance = currentBalance,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                BudgetAmount = budgetAmount
            };
        }
    }
}
