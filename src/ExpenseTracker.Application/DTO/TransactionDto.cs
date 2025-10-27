using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.DTO
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime TransactedOn { get; set; }
    }
}
