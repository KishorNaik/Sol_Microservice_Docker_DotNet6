using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.API.DTO.Response
{
    [Keyless]
    public class CustomerAuthResponseDTO
    {
        public Guid? CustomerID { get; set; }

        public string? EmailID { get; set; }

        public string? FullName { get; set; }

        public string? Hash { get; set; }

        public string? Salt { get; set; }

        [NotMapped]
        public string? JwtToken { get; set; }
    }
}