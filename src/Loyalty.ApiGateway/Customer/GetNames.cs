using Ardalis.ApiEndpoints;
using AutoMapper;
using CustomerApp.Api.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Loyalty.ApiGateway.Customer
{
    // Try to keep your endpoints as simple as possible. You can do simple validations or something similar.
    // But, keep the logic outside into separate handlers, proxies, etc.
    // Since this is a gateway in the distruted variant, it will utilize various external resources.
    // Encapsulating the actions in separate constructs, we can decorate them with various global caching mechanisms (e.g. Redis), retry policies, circuit breakers, etc.
    public class GetNames : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<string>
    {
        private readonly IMapper mapper;
        private readonly ICustomerService customerService;

        public GetNames(IMapper mapper,
                   ICustomerService customerService)
        {
            this.mapper = mapper;
            this.customerService = customerService;
        }

        [HttpGet("/customernames")]
        [SwaggerOperation(
            Summary = "Get customers names",
            Description = "Get customers names",
            Tags = new[] { "CustomerNamesEndpoint" })
        ]
        public override async Task<ActionResult<string>> HandleAsync(CancellationToken cancellationToken)
        {
            // This is just a sample. Usually we'll implement cancellation token throughout the call chain.
            var result = await customerService.GetCustomerNames();

            return Ok(result);
        }
    }
}
