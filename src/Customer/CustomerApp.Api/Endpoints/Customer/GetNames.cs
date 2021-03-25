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

namespace CustomerApp.Api.Endpoints.Customer
{
    // Once you decide to separate this component, all you need is just to define these dummy endpoints.
    // You'll utilize the already defined services. So, not much work to do.
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

        [HttpGet("/api/customernames")]
        [SwaggerOperation(
            Summary = "Get customers names",
            Description = "Get customers names",
            Tags = new[] { "CustomerApiEndpoint" })
        ]
        public override async Task<ActionResult<string>> HandleAsync(CancellationToken cancellationToken)
        {
            // This is just a sample. Usually we'll implement cancellation token throughout the call chain.
            var result = await customerService.GetCustomerNames();

            return Ok(result);
        }
    }
}
