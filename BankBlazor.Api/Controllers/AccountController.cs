using BankBlazor.Shared.DTOs;
using BankBlazor.Api.Enums;
using BankBlazor.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService; 
        public AccountController(IAccountService accountService) // Jag skapar en konstruktor som tar emot ICustomerService
        {
            _accountService = accountService;
        }

        // GET: api/AccountController/customer/5
        [HttpGet("account/{customerId}")] // Detta är en GET metod som hämtar alla konton för en specifik kund
        public async Task<ActionResult<List<AccountReadDTO>>> GetAccountsByCustomerId(int customerId)
        {
            var accounts = await _accountService.GetAccountByCustomerId(customerId);
            return Ok(accounts);
        }

        // GET: api/AccountController/balance/5
        [HttpGet("balance/{accountId}")]
        public async Task<ActionResult<decimal>> GetBalance(int accountId)
        {
            var balance = await _accountService.GetBalance(accountId);
            if (balance == null)
                return NotFound();

            return Ok(balance);
        }


    }
    
}

