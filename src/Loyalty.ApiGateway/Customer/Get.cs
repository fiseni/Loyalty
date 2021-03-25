using Ardalis.ApiEndpoints;
using AutoMapper;
using CustomerApp.Api.Contracts;
using Loyalty.ApiGateway.Proxies;
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
    public class Get : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<CustomerResult>
    {
        private readonly ICustomerProxy proxy;

        public Get(ICustomerProxy proxy)
        {
            this.proxy = proxy;
        }

        [HttpGet("/customers")]
        [SwaggerOperation(
            Summary = "Get all customers with contacts",
            Description = "Get all customers with contacts",
            Tags = new[] { "CustomerEndpoint" })
        ]
        public override async Task<ActionResult<CustomerResult>> HandleAsync(CancellationToken cancellationToken)
        {
            // This is just a sample. Usually we'll implement cancellation token throughout the call chain.
            var result = await proxy.GetCustomers();

            return Ok(result);
        }
    }
}
