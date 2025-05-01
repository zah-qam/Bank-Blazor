using BankBlazor.Api.DTOs;

namespace BankBlazor.Api.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<List<TransactionReadDTO>> GetByAccountId(int accountId);
        Task<TransactionReadDTO> CreateAsync(TransactionCreateDTO dto); // Skapa en ny transaktion
    }
}

