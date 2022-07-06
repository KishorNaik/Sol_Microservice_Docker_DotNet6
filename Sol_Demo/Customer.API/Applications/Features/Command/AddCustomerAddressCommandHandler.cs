using Customer.API.Infrastructures.DataService.Command;

namespace Customer.API.Applications.Features.Command
{
    public class AddCustomerAddressCommand : AddCustomerAddressRequestDTO, IRequest<Results<bool>>
    {
    }

    public class AddCustomerAddressCommandHandler : IRequestHandler<AddCustomerAddressCommand, Results<bool>>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public AddCustomerAddressCommandHandler(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        async Task<Results<bool>> IRequestHandler<AddCustomerAddressCommand, Results<bool>>.Handle(AddCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var flag = await this.mediator.Send<bool>(this.mapper.Map<AddCustomerAddressDataServiceCommand>(request));

                return new Results<bool>()
                {
                    Success = true,
                    StatusCode = 200,
                    Value = flag
                };
            }
            catch
            {
                throw;
            }
        }
    }
}