using Microsoft.EntityFrameworkCore;

namespace Customer.API.Infrastructures.DatabaseContext
{
    public partial class CustomersContext
    {
        public virtual DbSet<CustomerAuthResponseDTO> CustomerAuthResponse { get; set; } = null;
    }
}