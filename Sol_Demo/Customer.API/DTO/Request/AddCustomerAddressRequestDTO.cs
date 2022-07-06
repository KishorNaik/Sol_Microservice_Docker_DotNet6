namespace Customer.API.DTO.Request
{
    public class AddCustomerAddressRequestDTO
    {
        public Guid? CustomerId { get; set; }

        public String? FlatNo { get; set; }

        public String? Apartment { get; set; }

        public String? Street { get; set; }

        public String? LandMark { get; set; }

        public String? City { get; set; }

        public String? State { get; set; }

        public String? PinCode { get; set; }

        public bool? IsDeliveryAddress { get; set; }
    }
}