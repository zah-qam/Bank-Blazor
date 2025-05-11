namespace BankBlazor.Client.ViewModel
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string GivenName { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string Streetaddress { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string? Emailaddress { get; set; }
        public List<AccountViewModel> Accounts { get; set; } = new();
    }

    public class AccountViewModel
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public string? Frequency { get; set; }
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

