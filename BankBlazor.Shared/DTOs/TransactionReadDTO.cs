namespace BankBlazor.Shared.DTOs
{
    public class TransactionReadDTO
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; } // Vilket konto transaktionen tillhör
        public decimal Amount { get; set; } // Beloppet för transaktionen
        public DateOnly Date { get; set; }
        public string Type { get; set; } = string.Empty; // "Deposit", "Withdraw", "Transfer"
        public string Operation { get; set; } = string.Empty; // Valfri textbeskrivning för överföringar
        public decimal Balance { get; set; } // Saldo efter transaktionen
        public string? Bank { get; set; } // Kontot som pengarna överförs till (om det är en överföring)
        public string? Account { get; set; } // Kontonamnet (om det är en överföring)
        public string? Symbol { get; set; }


    }
}
