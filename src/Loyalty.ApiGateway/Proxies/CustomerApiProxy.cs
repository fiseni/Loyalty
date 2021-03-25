using Loyalty.ApiGateway.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Loyalty.ApiGateway.Proxies
{
    // Proxies are good place to decorate the actions with various global caching mechanisms (e.g. Redis), retry policies, circuit breakers, etc.
    // Avoid using complex logic in controllers, instead keep it here and additionally if needed on various other constructs.
    // This implementation of ICustomerProxy is making external calls to the Customer component (distributed variant).
    public class CustomerApiProxy : ICustomerProxy
    {
        // This is just a sample. In production we may want to use some retry policies, circuit breakers (Polly is a nice package).
        public Task<List<CustomerResult>> GetCustomers()
        {
            var client = new HttpClient();
            return client.GetFromJsonAsync<List<CustomerResult>>("https://localhost:6001/api/customers");
        }

        public Task<List<string>> GetCustomerNames()
        {
            var client = new HttpClient();
            return client.GetFromJsonAsync<List<string>>("https://localhost:6001/api/customernames");
        }
    }
}
