namespace BankBlazor.Api.DTOs
{
    public class TransactionCreateDTO
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty; // "Deposit", "Withdraw", "Transfer"
        public string Operation { get; set; } = string.Empty; // Valfri textbeskrivning för överföringar
        public string? Bank { get; set; }
        
    }
}

