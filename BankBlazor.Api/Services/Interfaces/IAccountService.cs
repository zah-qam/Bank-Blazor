using BankBlazor.Api.Data;
using BankBlazor.Api.DTOs;
using BankBlazor.Api.Enums;

namespace BankBlazor.Api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountReadDTO>> GetAccountByCustomerId(int customerId); 
        Task<decimal?> GetBalance(int accountId);
        Task<ResponseCode> Transfer(int fromAccountId, int toAccountId, decimal amount);
        Task<ResponseCode> Deposit(int accountId, decimal amount);
        Task<ResponseCode> Withdraw(int accountId, decimal amount);
    }
}
