using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.DTO;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Interfaces;
using TCW.Utility;

namespace ExpenseTracker.Application.Services
{
    public class TransactionService(ITransactionRepository transactionRepository,UtilsService utilsService):ITransactionService
    {
        public async Task<ServiceResponseDto<int>> AddTransactionAsync(TransactionDto transactionDto, CancellationToken cancellationToken)
        {
            var transactionType = await transactionRepository.GetTransactionTypeById(transactionDto.TransactionTypeId,cancellationToken);
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
                UserId=utilsService.CurrentUserId
            };
           var id= await transactionRepository.AddTransactionAysnc(transaction,cancellationToken);
            return new ServiceResponseDto<int>
            {
                IsSuccess = true,
                Data = id,
                Message = "Transaction Saved Successfully"
            };
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
        public async Task<List<DropDownDto>> GetTransactionTypes(CancellationToken cancellationToken)
        {
            var types= await transactionRepository.GetTransactionTypesAsync(cancellationToken);
            return types.Select(x=>new DropDownDto
            {
                Key=x.Id,
                Value=x.Type
            }).ToList();
        }
        public async Task<List<DropDownDto>> GetAccountsTypes(CancellationToken cancellationToken)
        {
            var types= await transactionRepository.GetAccountsAsync(cancellationToken);
            return types.Select(x=>new DropDownDto
            {
                Key=x.Id,
                Value=x.AccountName
            }).ToList();
        }
    }
}
