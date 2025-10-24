using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.DTO
{
    public class AppSettingDto
    {
        public string JWTKey { get; set; }
        public string DomainUrl { get; set; }

    }
}
