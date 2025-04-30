using BankBlazor.Api.Data;
using BankBlazor.Api.DTOs;
using BankBlazor.Api.Enums;
using BankBlazor.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BankBlazor.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankBlazorContext _dbContext;
        public AccountService(BankBlazorContext context)
        {
            _dbContext = context;
        }
        public async Task<List<AccountReadDTO>> GetAccountByCustomerId(int customerId)
        {
            var accounts = await _dbContext.Dispositions
                .Where(d => d.CustomerId == customerId) //d är dispositionen och d.AccountId är id:t på kontot
                .Select(d => new AccountReadDTO
                {
                    AccountId = d.Account.AccountId, // d.Accoun.AccountId är id:t på kontot
                    Balance = d.Account.Balance, // d.Account.Balance är saldot på kontot
                    Frequency = d.Account.Frequency, // d.Account.Frequency är frekvensen på kontot
                })
                .ToListAsync();
            return accounts;
        }
        public async Task<decimal?> GetBalance(int accountId) // Hämtar saldot, Vi letar fram kontot från databasen om vi hittar det,
                                                              // returnerar vi dess Balance om vi inte hittar det, returnerar vi null.
        {
            var account = await _dbContext.Accounts.FindAsync(accountId);
            return account?.Balance;
        }
        public async Task<ResponseCode> Transfer(int fromAccountId, int toAccountId, decimal amount) // Vi letar fram kontot från databasen, om vi hittar det,
                                                                                                // kollar vi om det har tillräckligt med saldo för att göra överföringen
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                
                var fromAccount = await _dbContext.Accounts.FindAsync(fromAccountId); // Letar fram kontot som ska överföra pengar
                var toAccount = await _dbContext.Accounts.FindAsync(toAccountId); // kontot som ska ta emot pengarna
                if (fromAccount == null || toAccount == null)
                {
                    return ResponseCode.NotFound; // Om något av kontona inte finns, returnera NotFound
                }
                if (fromAccount.Balance < amount) // Kollar om det finns tillräckligt med saldo
                {
                    return ResponseCode.InsufficientFunds; // Om det inte finns tillräckligt med saldo, returnera InsufficientFunds
                }
                fromAccount.Balance -= amount;
                    toAccount.Balance += amount;
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync(); // Ångrar allt om något fel händer.
                    return ResponseCode.Accepted; // Returnerar true om vi lyckas
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<ResponseCode> Deposit(int accountId, decimal amount) // Vi letar fram kontot från databasen, om vi hittar det,
                                                                       // lägger vi till beloppet på kontot och sparar ändringen i databasen
        {
            var account = await _dbContext.Accounts.FindAsync(accountId); // Letar oss att hämta kontot från databasen
            if (account == null)
            {
                return ResponseCode.NotFound;
            }
            account.Balance += amount;
            await _dbContext.SaveChangesAsync();
            return ResponseCode.Accepted; // Returnerar true om vi lyckas
        }
        public async Task<ResponseCode> Withdraw(int accountId, decimal amount)
        {
            var account = await _dbContext.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return ResponseCode.NotFound;
            }
            if (account.Balance < amount)
            {
                return ResponseCode.InsufficientFunds;
            }
            account.Balance -= amount;
            await _dbContext.SaveChangesAsync();
            return ResponseCode.Accepted;
        }
    }
}
