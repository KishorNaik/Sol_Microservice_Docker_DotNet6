using Customer.API.Business.Rule;
using Customer.API.Infrastructures.DataService.Command;
using System.Diagnostics;

namespace Customer.API.Applications.Features.Command
{
    public class RegisterCustomerCommand : CreateCustomerRequetsDTO, IRequest<Results<bool>>
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