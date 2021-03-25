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
    public class Get : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<CustomerResult>
    {
        private readonly IMapper mapper;
        private readonly ICustomerService customerService;

        public Get(IMapper mapper,
                   ICustomerService customerService)
        {
            this.mapper = mapper;
            this.customerService = customerService;
        }

        [HttpGet("/api/customers")]
        [SwaggerOperation(
            Summary = "Get all customers with contacts",
            Description = "Get all customers with contacts",
            Tags = new[] { "CustomerEndpoint" })
        ]
        public override async Task<ActionResult<CustomerResult>> HandleAsync(CancellationToken cancellationToken)
        {
            // This is just a sample. Usually we'll implement cancellation token throughout the call chain.
            var customersDto = await customerService.GetCustomers();
            var result = mapper.Map<List<CustomerResult>>(customersDto);

            return Ok(result);
        }
    }
}
