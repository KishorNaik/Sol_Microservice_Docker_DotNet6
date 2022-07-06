using System;
using System.Collections.Generic;

namespace Customer.API.Infrastructures.DatabaseContext
{
    public partial class Login
    {
        public decimal Id { get; set; }
        public Guid? LoginId { get; set; }
        public string? EmailId { get; set; }
        public string? Salt { get; set; }
        public string? Hash { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
