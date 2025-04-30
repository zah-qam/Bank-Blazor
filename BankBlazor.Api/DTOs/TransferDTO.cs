﻿namespace BankBlazor.Api.DTOs
{
    public class TransferDTO
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
