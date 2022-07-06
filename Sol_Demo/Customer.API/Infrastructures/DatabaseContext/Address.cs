using System;
using System.Collections.Generic;

namespace Customer.API.Infrastructures.DatabaseContext
{
    public partial class Address
    {
        public decimal Id { get; set; }
        public Guid? AddressId { get; set; }
        public string? FlatNo { get; set; }
        public string? Apartment { get; set; }
        public string? Street { get; set; }
        public string? LandMark { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Pincode { get; set; }
        public bool? IsActive { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
