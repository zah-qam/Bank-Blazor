namespace BankBlazor.Api.DTOs
{
    public class AccountReadDTO // Det som kommer tillbaka till klienten, och som klienten kan se,
                                // som kommer att visas i UI.
    {
        public int AccountId { get; set; }
        public string Frequency { get; set; } = null!;
        public DateOnly Created { get; set; }
        public decimal Balance { get; set; }
    }
}
