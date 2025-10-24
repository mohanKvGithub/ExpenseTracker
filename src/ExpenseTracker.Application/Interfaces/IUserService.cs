using ExpenseTracker.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Application.Interfaces
{
    public interface IUserService
    {
         Task<ServiceResponseDto<string>>AuthenticateUserAsync(UserDto userDto);
    }
}
