using System.Diagnostics;
using ExpenseTracker.Domain.Interfaces;
using ExpenseTracker.Models;
using ExpenseTracker.Application.Interfaces;
using ExpenseTracker.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class HomeController(IUserService userService) : Controller
    {

        public async Task<IActionResult> Login()
        {
           var userDto = new UserDto
            {
                Email = "kvm8105@gmail.com",
                Password = "Sys123"
            };
            var response = await userService.AuthenticateUserAsync(userDto);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserDto userDto)
        {
            var response=await userService.AuthenticateUserAsync(userDto);
            return Json(new { success = response.IsSuccess, message = response.Message });
        }
    }
}
