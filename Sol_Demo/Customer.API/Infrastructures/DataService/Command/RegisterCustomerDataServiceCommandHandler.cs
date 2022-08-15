using Customer.API.Infrastructures.DatabaseContext;

namespace Customer.API.Infrastructures.DataService.Command
{
    public class RegisterCustomerDataServiceCommand : RegisterCustomerRequetsDTO, IRequest<Guid?>
    {
        public string? Salt;

        public string? Hash;
    }

    public class RegisterCustomerDataServiceCommandHandler : IRequestHandler<RegisterCustomerDataServiceCommand, Guid?>
    {
        private readonly CustomersContext customersContext;
        private readonly IMapper mapper;

        public RegisterCustomerDataServiceCommandHandler(CustomersContext customersContext, IMapper mapper)
        {
            this.customersContext = customersContext;
            this.mapper = mapper;
        }

        async Task<Guid?> IRequestHandler<RegisterCustomerDataServiceCommand, Guid?>.Handle(RegisterCustomerDataServiceCommand request, CancellationToken cancellationToken)
        {
            using var transaction = await this.customersContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                Customers customer = this.mapper.Map<Customers>(request);
                Communication communication = this.mapper.Map<Communication>(request);
                Login login = this.mapper.Map<Login>(request);

                customer.CustomerId = Guid.NewGuid();

                await customersContext.Customers.AddAsync(customer);
                await customersContext.SaveChangesAsync();

                communication.CommunicationId = Guid.NewGuid();
                communication.CustomerId = customer.CustomerId;

                await customersContext.Communications.AddAsync(communication);
                await customersContext.SaveChangesAsync();

                login.LoginId = Guid.NewGuid();
                login.CustomerId = customer.CustomerId;
                login.Hash = request.Hash;
                login.Salt = request.Salt;

                await customersContext.Logins.AddAsync(login);
                await customersContext.SaveChangesAsync();

                await transaction.CommitAsync(cancellationToken);

                return customer?.CustomerId;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
            finally
            {
                await this.customersContext.DisposeAsync();
            }
        }
    }
}