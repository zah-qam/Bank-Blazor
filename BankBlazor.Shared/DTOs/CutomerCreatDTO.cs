namespace BankBlazor.Shared.DTOs
{
    public class CutomerCreatDTO //Detta är det som kommer in i API:et, som användaren fyller i ett formulär, och som vi ska använda för att skapa en ny kund.
    {
        public string Gender { get; set; } = null!;
        public string Givenname { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Streetaddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Zipcode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string? Emailaddress { get; set; }
        public string? Telephonenumber { get; set; }
        public DateOnly? Birthday { get; set; }
    }
}
