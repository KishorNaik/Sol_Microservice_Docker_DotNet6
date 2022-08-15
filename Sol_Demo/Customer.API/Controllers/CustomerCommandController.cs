using Customer.API.Applications.Features.Command;
using Customer.API.Applications.Features.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Customer.API.Controllers
{
    [Produces("application/json")]
    [Route("api/customer")]
    [ApiController]
    public class CustomerCommandController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public CustomerCommandController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer([FromBody] RegisterCustomerRequetsDTO createCustomerRequetsDTO)
        {
            Results<bool> result = await this.mediator.Send<Results<bool>>(this.mapper.Map<RegisterCustomerCommand>(createCustomerRequetsDTO));

            return base.StatusCode((int)result.StatusCode!, result);
        }

        [HttpPost("add-address")]
        [Authorize]
        public async Task<IActionResult> AddAddress([FromBody] AddCustomerAddressRequestDTO addCustomerAddressRequestDTO)
        {
            Results<bool> result = await this.mediator.Send<Results<bool>>(this.mapper.Map<AddCustomerAddressCommand>(addCustomerAddressRequestDTO));

            return base.StatusCode((int)result.StatusCode!, result);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CustomerAuthRequestDTO customerAuthRequestDTO)
        {
            Results<CustomerAuthResponseDTO> results = await this.mediator.Send<Results<CustomerAuthResponseDTO>>(this.mapper.Map<CustomerAuthQuery>(customerAuthRequestDTO));

            return base.StatusCode((int)results.StatusCode!, results);
        }
    }
}