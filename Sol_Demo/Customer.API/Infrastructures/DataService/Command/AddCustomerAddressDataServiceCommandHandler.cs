using Customer.API.Infrastructures.DatabaseContext;

namespace Customer.API.Infrastructures.DataService.Command
{
    public class AddCustomerAddressDataServiceCommand : AddCustomerAddressRequestDTO, IRequest<bool>
    {
    }

    public class AddCustomerAddressDataServiceCommandHandler : IRequestHandler<AddCustomerAddressDataServiceCommand, bool>
    {
        private readonly CustomersContext customersContext;
        private readonly IMapper mapper;

        public AddCustomerAddressDataServiceCommandHandler(CustomersContext customersContext, IMapper mapper)
        {
            this.customersContext = customersContext;
            this.mapper = mapper;
        }

        async Task<bool> IRequestHandler<AddCustomerAddressDataServiceCommand, bool>.Handle(AddCustomerAddressDataServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Address address = mapper.Map<Address>(request);
                address.AddressId = Guid.NewGuid();

                await this.customersContext.Addresses.AddAsync(address);
                await this.customersContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}