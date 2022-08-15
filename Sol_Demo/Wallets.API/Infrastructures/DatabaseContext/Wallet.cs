using System;
using System.Collections.Generic;

namespace Wallets.API.Infrastructure.DatabaseContext
{
    public partial class Wallet
    {
        public decimal WalletId { get; set; }
        public Guid WalletIdentifier { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifedDate { get; set; }
        public Guid CustomerIdentifier { get; set; }
    }
}