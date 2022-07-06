using System;
using System.Collections.Generic;

namespace Customer.API.Infrastructures.DatabaseContext
{
    public partial class Customer
    {
        public decimal Id { get; set; }
        public Guid? CustomerId { get; set; }
        public string? FullName { get; set; }
    }
}
