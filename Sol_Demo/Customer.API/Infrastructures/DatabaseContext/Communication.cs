using System;
using System.Collections.Generic;

namespace Customer.API.Infrastructures.DatabaseContext
{
    public partial class Communication
    {
        public decimal Id { get; set; }
        public Guid? CommunicationId { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public Guid? CustomerId { get; set; }
    }
}