using BankBlazor.Shared.DTOs;
using BankBlazor.Api.Enums;
using BankBlazor.Api.Services;
using BankBlazor.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace BankBlazor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            if (!transactions.Any()) return NotFound("Inga transaktioner hittades för det angivna kontot.");
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
            try
            {
                await _transactionService.Transfer(dto);
                return Ok("Överföringen gick genom utmärkt.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Överföringen misslyckades: {ex.Message}");
            }
        }

        // POST: api/AccountController/withdraw
        [HttpPost("withdraw")]
        public async Task<ActionResult> Withdraw(TransactionCreateDTO dto)
        {
            try
            {
                var result = await _transactionService.Withdraw(dto);
                return CreatedAtAction(nameof(GetTransactionsByAccountId), new { accountId = dto.AccountId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Uttaget misslyckades: {ex.Message}");
            }
        }

        // Post: api/AccountController/deposit
        [HttpPost("deposit")]
        public async Task<ActionResult<TransactionReadDTO>> Deposit(TransactionCreateDTO dto)
        {
            try
            {
                var result = await _transactionService.Deposit(dto);
                return CreatedAtAction(nameof(GetTransactionsByAccountId), new { accountId = dto.AccountId }, result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Insättningen misslyckades: {ex.Message}");
            }
        }
    }
}
