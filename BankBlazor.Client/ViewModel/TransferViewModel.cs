﻿namespace BankBlazor.Client.ViewModel
{
    public class TransferViewModel
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
