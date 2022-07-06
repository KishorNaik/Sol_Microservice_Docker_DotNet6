using Customer.API.Business.Rule;
using Customer.API.Infrastructures.DataService.Command;
using System.Diagnostics;

namespace Customer.API.Applications.Features.Command
{
    public class CreateCustomerCommand : CreateCustomerRequetsDTO, IRequest<Results<bool>>
    {
    }

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Results<bool>>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IHashPasswordRule hashPasswordRule;

        public CreateCustomerCommandHandler(IMediator mediator, IMapper mapper, IHashPasswordRule hashPasswordRule)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.hashPasswordRule = hashPasswordRule;
        }

        async Task<Results<bool>> IRequestHandler<CreateCustomerCommand, Results<bool>>.Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                (string? salt, string? hash) = await this.hashPasswordRule.CreatePasswordAsync(request.Password)!;

                CreateCustomerDataServiceCommand createCustomerDataServiceCommand = this.mapper.Map<CreateCustomerDataServiceCommand>(request);
                createCustomerDataServiceCommand.Hash = hash;
                createCustomerDataServiceCommand.Salt = salt;

                var flag = await this.mediator.Send<bool>(createCustomerDataServiceCommand);

                return new Results<bool>
                {
                    Success = true,
                    Value = flag,
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