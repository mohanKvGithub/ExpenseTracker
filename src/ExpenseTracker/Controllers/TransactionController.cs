using DataTables.Queryable;
using ExpenseTracker.Application.DTO;
using ExpenseTracker.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
namespace ExpenseTracker.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class TransactionController(ITransactionService transactionService) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            ViewBag.TransactionTypes = await transactionService.GetTransactionTypes(cancellationToken);
            ViewBag.AccountTypes = await transactionService.GetAccountsTypes(cancellationToken);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTransactionAsync(TransactionDto transactionDto, CancellationToken cancellationToken)
        {
            var response = await transactionService.AddTransactionAsync(transactionDto, cancellationToken);
            return Json(new { success = response.IsSuccess, message = response.Message });
        }
        [HttpGet]
        public async Task<IActionResult> GetTransactionsAsync(CancellationToken cancellationToken)
        {
            var request = new DataTablesRequest<TransactionDto>(Request.QueryString.ToString());
            var transactions=await transactionService.GetTransactionsAsync(cancellationToken);
            var transactionsFiltered=await transactions.ToPagedListAsync(request);
            return Json(new
            {
                draw = request.Draw,
                recordsTotal = transactionsFiltered.TotalCount,
                recordsFiltered = transactionsFiltered.TotalCount,
                data = transactionsFiltered
            });
        }
    }
}
