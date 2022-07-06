using Customer.API.Infrastructures.DatabaseContext;

namespace Customer.API.Infrastructures.DataService.Command
{
    public class CreateCustomerDataServiceCommand : CreateCustomerRequetsDTO, IRequest<bool>
    {
        public string? Salt;

        public string? Hash;
    }

    public class CreateCustomerDataServiceCommandHandler : IRequestHandler<CreateCustomerDataServiceCommand, bool>
    {
        private readonly CustomersContext customersContext;
        private readonly IMapper mapper;

        public CreateCustomerDataServiceCommandHandler(CustomersContext customersContext, IMapper mapper)
        {
            this.customersContext = customersContext;
            this.mapper = mapper;
        }

        async Task<bool> IRequestHandler<CreateCustomerDataServiceCommand, bool>.Handle(CreateCustomerDataServiceCommand request, CancellationToken cancellationToken)
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

                return true;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}