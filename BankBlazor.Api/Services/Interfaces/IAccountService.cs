using BankBlazor.Api.Data;

namespace BankBlazor.Api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountReadDTO>> GetAccountByCustomerId(int customerId); // Ska göra en AccountReadDTO.
        Task<decimal?> GetBalance(int accountId);
        Task<Account> Transfer(int fromAccountId, int toAccountId, decimal amount);
        Task<bool> Deposit(int accountId, decimal amount);
        Task<bool> Withdraw(int accountId, decimal amount);
    }
}
