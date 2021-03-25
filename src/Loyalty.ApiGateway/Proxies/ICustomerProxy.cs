using Loyalty.ApiGateway.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loyalty.ApiGateway.Proxies
{
    // Proxies are good place to decorate the actions with various global caching mechanisms (e.g. Redis), retry policies, circuit breakers, etc.
    // Avoid using complex logic in controllers, instead keep it here and additionally if needed on various other constructs.
    // Having this interface will enable us to switch the implementations (inProcess or Http calls) on the fly.
    // The rest of the logic in the ApiGateway, if any other constructs requires some customer information, should utilize ICustomerProxy, not the implementations.
    // The implementations can be even defined through external configurations.
    // https://fiseni.com/posts/open-close-principle-and-runtime-di-configuration/
    public interface ICustomerProxy
    {
        Task<List<CustomerResult>> GetCustomers();
        Task<List<string>> GetCustomerNames();
    }
}
