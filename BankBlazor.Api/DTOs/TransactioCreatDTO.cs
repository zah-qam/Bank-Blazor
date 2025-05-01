namespace BankBlazor.Api.DTOs
{
    public class TransactioCreatDTO
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public int? TargetAccountId { get; set; }
        public string Type { get; set; } = string.Empty; // "Deposit", "Withdraw", "Transfer"
    }
}

