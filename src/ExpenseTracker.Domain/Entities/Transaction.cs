using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public partial class Transaction:Common
    {
        public int UserId {  get; set; }
        public int TransactionTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Dr { get; set; }
        public decimal Cr { get; set; }
        public DateTime TransactedOn { get; set; }
        public virtual TransactionType TransactionType { get; set; } = null!;
        public virtual PaymentType PaymentType { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
