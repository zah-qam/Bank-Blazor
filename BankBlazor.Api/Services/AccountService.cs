using BankBlazor.Api.Data;
using BankBlazor.Shared.DTOs;
using BankBlazor.Api.Enums;
using BankBlazor.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BankBlazor.Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankBlazorContext _dbContext; // Vi kommer att använda oss av BankBlazorContext för att hämta och spara data i databasen
        private readonly ITransactionService _transactionService; // Vi kommer att använda oss av TransactionService för att skapa transaktioner
        public AccountService(BankBlazorContext context, ITransactionService transactionService) // Vi injicerar TransactionService i AccountService
        {
            _dbContext = context;
            _transactionService = transactionService;
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
       
    }
}
