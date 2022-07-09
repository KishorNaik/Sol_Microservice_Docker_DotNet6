using Customer.API.Business.Rule;
using Customer.API.Infrastructures.DataService.Query;
using System.Reflection.Metadata.Ecma335;

namespace Customer.API.Applications.Features.Query
{
    public class CustomerAuthQuery : CustomerAuthRequestDTO, IRequest<Results<CustomerAuthResponseDTO>>
    {
    }

    public class CustomerAuthQueryHandler : IRequestHandler<CustomerAuthQuery, Results<CustomerAuthResponseDTO>>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly IHashPasswordRule hashPasswordRule;
        private readonly IJwtGeneratorRule jwtGeneratorRule;

        public CustomerAuthQueryHandler(IMediator mediator, IMapper mapper, IHashPasswordRule hashPasswordRule, IJwtGeneratorRule jwtGeneratorRule)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.hashPasswordRule = hashPasswordRule;
            this.jwtGeneratorRule = jwtGeneratorRule;
        }

        async Task<Results<CustomerAuthResponseDTO>> IRequestHandler<CustomerAuthQuery, Results<CustomerAuthResponseDTO>>.Handle(CustomerAuthQuery request, CancellationToken cancellationToken)
        {
            try
            {
                CustomerAuthDataServiceQuery customerAuthDataServiceQuery = this.mapper.Map<CustomerAuthDataServiceQuery>(request);

                CustomerAuthResponseDTO customerAuthResponseDTO = await this.mediator.Send(customerAuthDataServiceQuery, cancellationToken);

                if (customerAuthResponseDTO != null)
                {
                    // Check Hash & Salt
                    var flag = await this.hashPasswordRule.ValidatePassword(request.Password, customerAuthResponseDTO.Salt, customerAuthResponseDTO.Hash)!;

                    if (flag == true)
                    {
                        customerAuthResponseDTO.Salt = null;
                        customerAuthResponseDTO.Hash = null;

                        // Generate JWT Token
                        var jwtToken = await this.jwtGeneratorRule.GenerateJwtTokenAsync(customerAuthResponseDTO.CustomerID);
                        customerAuthResponseDTO.JwtToken = jwtToken;

                        return Response("User Name & Password is match", true, customerAuthResponseDTO);
                    }
                    else
                    {
                        return Response("User Name & Password does not match", false, null);
                    }
                }
                else
                {
                    return Response("User Name & Password does not match", false, null);
                }

                // Create a Response (Inline funtion)
                Results<CustomerAuthResponseDTO> Response(string? message, bool? success, CustomerAuthResponseDTO? _customerAuthResponseDTO)
                {
                    return new Results<CustomerAuthResponseDTO>()
                    {
                        StatusCode = 200,
                        Success = success,
                        Message = message,
                        Value = _customerAuthResponseDTO
                    };
                }
            }
            catch (Exception ex)
            {
                return new Results<CustomerAuthResponseDTO>()
                {
                    StatusCode = 500,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}