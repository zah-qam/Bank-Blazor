using BankBlazor.Api.DTOs;
using BankBlazor.Api.Enums;

namespace BankBlazor.Api.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionReadDTO>> GetByAccountId(int accountId);
        Task<TransactionReadDTO> CreateAsync(TransactionCreateDTO dto); // Skapa en ny transaktion
        Task Transfer(TransferDTO transferDTO);
        Task<TransactionReadDTO> Deposit(TransactionCreateDTO ttansactionDTO);
        Task<TransactionReadDTO> Withdraw(TransactionCreateDTO transactionDTO);
    }
}

