﻿namespace RoboBank.Account.Service.NetCore.Models
{
    public class TransferModel
    {
        public string SourceAccountId { get; set; }

        public string TargetAccountId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }
    }
}
