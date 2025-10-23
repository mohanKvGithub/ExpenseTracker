using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class BudgetReset:Common
    {
        public int UserId { get; set; }
        public DateTime ResetDate { get; set; }
    }
}
