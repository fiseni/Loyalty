using AutoMapper;
using CustomerApp.Api.Contracts;
using Loyalty.ApiGateway.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loyalty.ApiGateway.Proxies
{
    // Proxies are good place to decorate the actions with various global caching mechanisms (e.g. Redis), retry policies, circuit breakers, etc.
    // Avoid using complex logic in controllers, instead keep it here and additionally if needed on various other constructs.
    // This implementation of ICustomerProxy is working directly with the services provided in the Customer component (monolithic variant).
    public class CustomerProxy : ICustomerProxy
    {
        private readonly IMapper mapper;
        private readonly ICustomerService customerService;

        public CustomerProxy(IMapper mapper,
                             ICustomerService customerService)
        {
            this.mapper = mapper;
            this.customerService = customerService;
        }

        public async Task<List<CustomerResult>> GetCustomers()
        {
            var customers = await customerService.GetCustomers();

            return mapper.Map<List<CustomerResult>>(customers);
        }

        public Task<List<string>> GetCustomerNames()
        {
            return customerService.GetCustomerNames();
        }
    }
}
