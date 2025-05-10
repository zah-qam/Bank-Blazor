
using BankBlazor.Api.Data;
using BankBlazor.Api.DTOs;
using BankBlazor.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankBlazor.Api.Services
{
    public class TransactionService : ITransactionService // AccountService ansvarar då för kontodata (hämta saldon, lista konton).
                                                          // TransactionService ansvarar för transaktionshistorik =>
                                                          // skapa transaktioner, hämta alla transaktioner för ett konto, etc.
    {
        private readonly BankBlazorContext _dbContext;
        public TransactionService(BankBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }

//<<<<<<< HEAD
//=======
        // En metod för att hämta alla transaktioner för ett konto

//>>>>>>> Services
        public async Task<List<TransactionReadDTO>> GetByAccountId(int accountId)
        {
            return await _dbContext.Transactions
                .Where(t => t.AccountId == accountId)
                .Select(t => new TransactionReadDTO
                {
                    TransactionId = t.TransactionId,
                    AccountId = t.AccountId,
                    Amount = t.Amount,
                    Date = t.Date,
                    Type = t.Type,
                    Operation = t.Operation,
                    Balance = t.Balance,
                    Bank = t.Bank,

                })
                .ToListAsync();
        }

        public async Task<TransactionReadDTO> CreateAsync(TransactionCreateDTO dto)
        {
            var account = await _dbContext.Accounts.FindAsync(dto.AccountId);
            if (account == null)
                throw new Exception("Kontot hittades inte.");

            // Uppdatera saldo
            if (dto.Type == "Deposit")
                account.Balance += dto.Amount;
            else if (dto.Type == "Withdraw")
            {
                if (account.Balance < dto.Amount)
                    throw new Exception("Otillräckligt saldo.");
                account.Balance -= dto.Amount;
            }
            var transaction = new Transaction
            {
                AccountId = dto.AccountId,
                Amount = dto.Amount,
                Date = DateOnly.FromDateTime(DateTime.Now), // Använder DateOnly eftersom din entity har det
                Type = dto.Type,
                Operation = dto.Operation,
                Balance = account.Balance,
                Bank = dto.Bank,
                Symbol = null, // Anpassa om du vill sätta
                Account = null // Normalt sätts detta automatiskt om navigation används
            };

            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            return new TransactionReadDTO
            {
                TransactionId = transaction.TransactionId,
                AccountId = transaction.AccountId,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Type = transaction.Type,
                Operation = transaction.Operation,
                Balance = transaction.Balance,
                Bank = transaction.Bank
            };
        }

    }
}
