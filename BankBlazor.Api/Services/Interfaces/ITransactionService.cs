using BankBlazor.Api.DTOs;
using BankBlazor.Api.Enums;

namespace BankBlazor.Api.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionReadDTO>> GetByAccountId(int accountId);
        Task<TransactionReadDTO> CreateAsync(TransactionCreateDTO dto); // Skapa en ny transaktion
        Task<ResponseCode> Transfer(int fromAccountId, int toAccountId, decimal amount);
        Task<ResponseCode> Deposit(int accountId, decimal amount);
        Task<ResponseCode> Withdraw(int accountId, decimal amount);
    }
}

