namespace BankBlazor.Api.DTOs
{
    public class TransactionReadDTO
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; } // Vilket konto transaktionen tillhör
        public decimal Amount { get; set; } // Beloppet för transaktionen
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; } = string.Empty; // Valfri textbeskrivning för överföringar
        public int? TargetAccountId { get; set; } // Kontot som pengarna överförs till (om det är en överföring)
        public string Type { get; set; } // "Deposit", "Withdraw", "Transfer"
        public bool IsDeposit { get; set; } // Om det är en insättning
    }
}
