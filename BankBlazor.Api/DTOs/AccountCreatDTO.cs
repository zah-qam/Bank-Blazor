namespace BankBlazor.Api.DTOs
{
    public class AccountCreatDTO //Detta är det som kommer in i API:et, som användaren fyller i ett
                                 //formulär, och som vi ska använda för att skapa en ny kund.
    {
        public string Frequency { get; set; } = null!;
        public DateOnly Created { get; set; }
        public decimal Balance { get; set; }
    }
}
