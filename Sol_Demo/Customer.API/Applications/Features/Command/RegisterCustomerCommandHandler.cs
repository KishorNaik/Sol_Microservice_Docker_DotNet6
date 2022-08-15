using Customer.API.Business.Rule;
using Customer.API.Infrastructures.DataService.Command;
using System.Diagnostics;

namespace Customer.API.Applications.Features.Command
{
    public class RegisterCustomerCommand : RegisterCustomerRequetsDTO, IRequest<Results<bool>>
    {
    }

    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, Results<bool>>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IHashPasswordRule hashPasswordRule;

        public RegisterCustomerCommandHandler(IMediator mediator, IMapper mapper, IHashPasswordRule hashPasswordRule)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.hashPasswordRule = hashPasswordRule;
        }

        async Task<Results<bool>> IRequestHandler<RegisterCustomerCommand, Results<bool>>.Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            bool? flag = false;
            try
            {
                (string? salt, string? hash) = await this.hashPasswordRule.CreatePasswordAsync(request.Password)!;

                RegisterCustomerDataServiceCommand createCustomerDataServiceCommand = this.mapper.Map<RegisterCustomerDataServiceCommand>(request);
                createCustomerDataServiceCommand.Hash = hash;
                createCustomerDataServiceCommand.Salt = salt;

                var customerIdentifer = await this.mediator.Send<Guid?>(createCustomerDataServiceCommand);

                if (customerIdentifer != null)
                {
                    await this.mediator.Publish<CreateWalletIntegrationProducerEvent>(new CreateWalletIntegrationProducerEvent()
                    {
                        CustomerIdentifier = customerIdentifer
                    });
                    flag = true;
                }
                else
                {
                    flag = false;
                }

                return new Results<bool>
                {
                    Success = true,
                    Value = Convert.ToBoolean(flag),
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new Results<bool>
                {
                    Success = false,
                    Message = ex.Message,
                    StatusCode = 500,
                };
            }
        }
    }
}