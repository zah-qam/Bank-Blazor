using BankBlazor.Api.DTOs;
using BankBlazor.Api.Enums;
using BankBlazor.Api.Services;
using BankBlazor.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.Api.Controllers
{
    [Route("api/[transaction]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/TransactionController
        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<List<TransactionReadDTO>>> GetTransactionsByAccountId(int accountId)
        {
            var transactions = await _transactionService.GetByAccountId(accountId);
            return Ok(transactions);
        }

        // POST: api/TransactionController
        [HttpPost]
        public async Task<ActionResult<TransactionReadDTO>> CreateTransaction([FromBody] TransactionCreateDTO dto)
        {
            var createdTransaction = await _transactionService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetTransactionsByAccountId), new { accountId = dto.AccountId }, createdTransaction);
        }


        // POST: api/AccountController/transfer
        [HttpPost("transfer")]
        public async Task<ActionResult> Transfer([FromBody] TransferDTO dto)
        {
            var result = await _transactionService.Transfer(dto.FromAccountId, dto.ToAccountId, dto.Amount);
            return result == ResponseCode.Success ? Ok("Överföringen gick genom utmärkt.") :
                   result == ResponseCode.NotFound ? NotFound("Någon/ingen av konton hittades inte.") :
                   BadRequest("Ej tillräckligt med saldo.");
        }

        // POST: api/AccountController/withdraw
        [HttpPost("withdraw")]
        public async Task<ActionResult> Withdraw([FromBody] TransactionCreateDTO dto)
        {
            var result = await _transactionService.Withdraw(dto.AccountId, dto.Amount);
            return result == ResponseCode.Success ? Ok("Transaktionen gick genom.") :
                   result == ResponseCode.NotFound ? NotFound("Kontot hittades ej.") :
                   BadRequest("Ej tillräckligt med saldo!");
        }

        // Post: api/AccountController/deposit
        [HttpPost("deposit")]
        public async Task<ActionResult> Deposit([FromBody] TransactionCreateDTO dto)
        {
            var result = await _transactionService.Deposit(dto.AccountId, dto.Amount);
            return result == ResponseCode.Success ? Ok("Överföringen gick genom utmärkt.") :
                   result == ResponseCode.NotFound ? NotFound("Någon/ ingen av kontona hittades inte.") :
                   BadRequest("Oväntat fel inträffade.");
        }
    }
}
