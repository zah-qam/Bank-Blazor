namespace BankBlazor.Client.ViewModel
{
    public class CustomerViewModel
    {

        public int CustomerId { get; set; }
        public string Givenname { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Emailaddress { get; set; } = string.Empty;
        public string Streetaddress { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public List<AccountViewModel> Accounts { get; set; } = new();
    }

    

    




}

