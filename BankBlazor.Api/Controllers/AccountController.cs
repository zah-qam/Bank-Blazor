using BankBlazor.Api.DTOs;
using BankBlazor.Api.Enums;
using BankBlazor.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.Api.Controllers
{
    [ApiController]
    [Route("api/[account]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService; 
        public AccountController(IAccountService accountService) // Jag skapar en konstruktor som tar emot ICustomerService
        {
            _accountService = accountService;
        }

        // GET: api/AccountController
        [HttpGet("customer/{customerId}")] // Detta är en GET metod som hämtar alla konton för en specifik kund
        public async Task<ActionResult<List<AccountReadDTO>>> GetAccountsByCustomerId(int customerId)
        {
            var accounts = await _accountService.GetAccountByCustomerId(customerId);
            return Ok(accounts);
        }

        [HttpGet("balance/{accountId}")]
        public async Task<ActionResult<decimal>> GetBalance(int accountId)
        {
            var balance = await _accountService.GetBalance(accountId);
            if (balance == null)
                return NotFound();

            return Ok(balance);
        }

        [HttpPost("transfer")]
        public async Task<ActionResult> Transfer([FromBody] TransferDTO dto) 
        {
            var result = await _accountService.Transfer(dto.FromAccountId, dto.ToAccountId, dto.Amount);
            return result == ResponseCode.Success ? Ok("Överföringen gick genom utmärkt.") :
                   result == ResponseCode.NotFound ? NotFound("Någon/ingen av konton hittades inte.") :
                   BadRequest("Ej tillräckligt med saldo.");
        }

        [HttpPost("withdraw")]
        public async Task<ActionResult> Withdraw([FromBody] TransactionDTO dto) 
        {
            var result = await _accountService.Withdraw(dto.AccountId, dto.Amount);
            return result == ResponseCode.Success ? Ok("Transaktionen gick genom.") :
                   result == ResponseCode.NotFound ? NotFound("Kontot hittades ej.") :
                   BadRequest("Ej tillräckligt med saldo!");
        }

        [HttpPost("deposit")]
        public async Task<ActionResult> Deposit([FromBody] TransactionDTO dto) 
        {
            var result = await _accountService.Deposit(dto.AccountId, dto.Amount);
            return result == ResponseCode.Success ? Ok("Överföringen gick genom utmärkt.") :
                   result == ResponseCode.NotFound ? NotFound("Någon/ ingen av kontona hittades inte.") :
                   BadRequest("Oväntat fel inträffade.");
        }

    }
    
}

