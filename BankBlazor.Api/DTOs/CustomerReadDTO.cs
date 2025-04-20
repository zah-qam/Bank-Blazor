﻿namespace BankBlazor.Api.DTOs
{
    public class CustomerReadDTO
    {
        public int CustomerId { get; set; }
        public string Givenname { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Streetaddress { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Zipcode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Emailaddress { get; set; }
        public string? Telephonenumber { get; set; }
    }
}
