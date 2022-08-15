using System.Runtime.Serialization;

namespace Customer.API.DTO.Request
{
    [DataContract]
    public class RegisterCustomerRequetsDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public String? FullName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String? EmailId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String? MobileNo { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String? Password { get; set; }
    }
}