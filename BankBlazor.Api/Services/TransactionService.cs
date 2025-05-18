
using BankBlazor.Api.Data;
using BankBlazor.Api.DTOs;
using BankBlazor.Api.Enums;
using BankBlazor.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankBlazor.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankBlazorContext _dbContext;

        public TransactionService(BankBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TransactionReadDTO>> GetByAccountId(int accountId)
        {
            return await _dbContext.Transactions
                .Where(t => t.AccountId == accountId)
                .Select(t => new TransactionReadDTO
                {
                    TransactionId = t.TransactionId,
                    Symbol = t.Symbol,
                    Amount = t.Amount,
                    Date = t.Date,
                    Type = t.Type,
                    Operation = t.Operation,
                    Balance = t.Balance,
                    Bank = t.Bank,
                    Account = t.Account
                })
                .ToListAsync();
        }

        public async Task<TransactionReadDTO> GetTransactionById(int transactionId)
        {
            var transaction = await _dbContext.Transactions
                .Where(t => t.TransactionId == transactionId)
                .Select(t => new TransactionReadDTO
                {
                    TransactionId = t.TransactionId,
                    Date = t.Date,
                    Type = t.Type,
                    Operation = t.Operation,
                    Amount = t.Amount,
                    Balance = t.Balance,
                    Symbol = t.Symbol,
                    Bank = t.Bank,
                    Account = t.Account
                })
                .FirstOrDefaultAsync();

            return transaction;
        }

        public async Task<TransactionReadDTO> CreateAsync(TransactionCreateDTO dto)
        {
            var account = await _dbContext.Accounts.FindAsync(dto.AccountId);
            if (account == null)
                throw new Exception("Kontot hittades inte.");

            var newBalance = account.Balance + dto.Amount;

            var transaction = new Transaction
            {
                AccountId = dto.AccountId,
                Amount = dto.Amount,
                Date = dto.Date,
                Type = "Credit", 
                Operation = "Deposit", 
                Balance = account.Balance,
                Bank = dto.Bank,
                Symbol = dto.Symbol,
                Account = dto.Account
            };
            account.Balance = newBalance;

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
                Bank = transaction.Bank,
                Symbol = transaction.Symbol,
                Account = transaction.Account
            };
        }

        public async Task Transfer(TransferDTO transferDto)
        {
            var fromAccount = await _dbContext.Accounts.FindAsync(transferDto.FromAccountId);
            var toAccount = await _dbContext.Accounts.FindAsync(transferDto.ToAccountId);

            if (fromAccount == null || toAccount == null)
                throw new Exception("Något av kontona hittades inte.");

            if (fromAccount.Balance < transferDto.Amount)
                throw new Exception("Ej tillräckligt med saldo.");

            fromAccount.Balance -= transferDto.Amount;
            toAccount.Balance += transferDto.Amount;

            var withdrawal = new Transaction
            {
                AccountId = fromAccount.AccountId,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Type = "Debit",
                Operation = "Transfer Out",
                Amount = -transferDto.Amount,
                Balance = fromAccount.Balance,
                Symbol = transferDto.Symbol,
                Bank = transferDto.Bank,
                Account = transferDto.Account
            };

            var deposit = new Transaction
            {
                AccountId = toAccount.AccountId,
                Date = DateOnly.FromDateTime(DateTime.Today),
                Type = "Credit",
                Operation = "Transfer In",
                Amount = amount,
                Balance = toAccount.Balance,
                Symbol = null,
                Bank = "BankBlazor",
                Account = null
            };

            _dbContext.Transactions.Add(withdrawal);
            _dbContext.Transactions.Add(deposit);

            await _dbContext.SaveChangesAsync();
            return ResponseCode.Accepted;
        }

        public async Task<ResponseCode> Deposit(int accountId, decimal amount)
        {
            var account = await _dbContext.Accounts.FindAsync(accountId);
            if (account == null) return ResponseCode.NotFound;

            account.Balance += amount;

            var transaction = new Transaction
            {
                AccountId = accountId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "Credit",
                Operation = "Deposit",
                Amount = amount,
                Balance = account.Balance,
                Bank = "BankBlazor",
                Symbol = null,
                Account = null
            };

            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();
            return ResponseCode.Accepted;
        }

        public async Task<ResponseCode> Withdraw(int accountId, decimal amount)
        {
            var account = await _dbContext.Accounts.FindAsync(accountId);
            if (account == null) return ResponseCode.NotFound;
            if (account.Balance < amount) return ResponseCode.InsufficientFunds;

            account.Balance -= amount;

            var transaction = new Transaction
            {
                AccountId = accountId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Type = "Withdraw",
                Operation = "Uttag",
                Amount = amount,
                Balance = account.Balance,
                Bank = "BankBlazor",
                Symbol = null,
                Account = null
            };

            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();
            return ResponseCode.Accepted;
        }
    }

}

