using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.DTO
{
    public class DashboardStatDto
    {
        public decimal CurrentBalance { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal BalanceAfterBudget=> CurrentBalance - BudgetAmount;
    }
}
