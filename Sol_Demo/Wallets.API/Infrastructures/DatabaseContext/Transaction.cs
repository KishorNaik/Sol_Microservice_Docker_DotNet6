using System;
using System.Collections.Generic;

namespace Wallets.API.Infrastructure.DatabaseContext
{
    public partial class Transaction
    {
        public decimal TransactionId { get; set; }
        public Guid TransactionIdentifer { get; set; }
        public string Type { get; set; } = null!;
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        public Guid WalletIdentifier { get; set; }
        public Guid CustomerIdentifier { get; set; }
        public Guid OrderIdentifier { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifedDate { get; set; }
    }
}