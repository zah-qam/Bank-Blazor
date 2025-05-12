using BankBlazor.Api.DTOs;
using BankBlazor.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.Api.Controllers
{
    [Route("api/[controller]")]
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
            if (!transactions.Any()) return NotFound();
            return Ok(transactions);
        }

        // POST: api/TransactionController
        [HttpPost]
        public async Task<ActionResult<TransactionReadDTO>> CreateTransaction([FromBody] TransactionCreateDTO dto)
        {
            var createdTransaction = await _transactionService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetTransactionsByAccountId), new { accountId = dto.AccountId }, createdTransaction);
        }
    }
}
