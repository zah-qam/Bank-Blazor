using BankBlazor.Api.Data;
using BankBlazor.Api.DTOs;
using BankBlazor.Api.Enums;

namespace BankBlazor.Api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<AccountReadDTO>> GetAccountByCustomerId(int customerId); 
        Task<decimal?> GetBalance(int accountId);
        
    }
}
