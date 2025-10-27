using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class TransactionType:Common
    {
        public string Type { get; set; }= string.Empty;
        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}
