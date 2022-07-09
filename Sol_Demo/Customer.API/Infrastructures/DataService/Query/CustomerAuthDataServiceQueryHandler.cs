using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Customer.API.Infrastructures.DataService.Query
{
    public class CustomerAuthDataServiceQuery : CustomerAuthRequestDTO, IRequest<CustomerAuthResponseDTO>
    {
    }

    public class CustomerAuthDataServiceQueryHandler : IRequestHandler<CustomerAuthDataServiceQuery, CustomerAuthResponseDTO>
    {
        private readonly CustomersContext customersContext;
        private readonly IMapper mapper;

        public CustomerAuthDataServiceQueryHandler(CustomersContext customersContext, IMapper mapper)
        {
            this.customersContext = customersContext;
            this.mapper = mapper;
        }

        async Task<CustomerAuthResponseDTO> IRequestHandler<CustomerAuthDataServiceQuery, CustomerAuthResponseDTO>.Handle(CustomerAuthDataServiceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Login login = this.mapper.Map<Login>(request);

                string query = $@"
                    select
                        c.CustomerID,
                        c.FullName,
                        l.EmailID,
                        l.[Hash],
                        l.Salt
                    from Customers as c
                    inner join
                        [Login] as l
                    on
                        l.CustomerID=c.CustomerID
                    where
                        l.EmailID=@emailId
                ";

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("emailId", login?.EmailId));

                // Get Customer Data
                var result = await this.customersContext
                    .CustomerAuthResponse
                    .FromSqlRaw(query, parameters?.Cast<Object>()?.ToArray()!)
                    .FirstOrDefaultAsync();

                return result!;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}