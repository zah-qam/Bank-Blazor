namespace BankBlazor.Client.ViewModel
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string Givenname { get; set; }
        public string Surname { get; set; }
        public string Streetaddress { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string Emailaddress { get; set; }
        public string Telephonenumber { get; set; }
        public List<AccountViewModel> Accounts { get; set; } = new();
    }

    public class AccountViewModel
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public string? Frequency { get; set; }
        public DateTime Created { get; set; }
    }

    public class TransactionViewModel
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = string.Empty; // "Deposit", "Withdraw", "Transfer"
        public string Operation { get; set; } = string.Empty; // Valfri textbeskrivning för överföringar
        public string? Bank { get; set; }
    }

    public class TransferViewModel
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }




}

