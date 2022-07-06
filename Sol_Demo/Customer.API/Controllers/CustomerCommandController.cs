using Customer.API.Applications.Features.Command;
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequetsDTO createCustomerRequetsDTO)
        {
            Results<bool> result = await this.mediator.Send<Results<bool>>(this.mapper.Map<CreateCustomerCommand>(createCustomerRequetsDTO));

            return base.StatusCode((int)result.StatusCode!, result);
        }

        [HttpPost("add-address")]
        public async Task<IActionResult> AddAddress([FromBody] AddCustomerAddressRequestDTO addCustomerAddressRequestDTO)
        {
            Results<bool> result = await this.mediator.Send<Results<bool>>(this.mapper.Map<AddCustomerAddressCommand>(addCustomerAddressRequestDTO));

            return base.StatusCode((int)result.StatusCode!, result);
        }
    }
}