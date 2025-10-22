using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.Entities
{
    public class User:Common
    {
        public string Name { get; set; }= string.Empty;
        public string Role { get; set; }= string.Empty;
        public string Password { get; set; }= string.Empty;
        public string Salt { get; set; }= string.Empty;
    }
}
