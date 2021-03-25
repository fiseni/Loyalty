using CustomerApp.Core.Contracts;
using CustomerApp.Core.Entities;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Infrastructure.Data
{
    // Usually you won't need separate interface ICustomerCachedRepository.
    // You'll implement ICustomerRepository, and accept the implementation CustomerRepository in constructor.
    // I have defined it separately just to demonstrate various usages in CustomerService.
    // Various approaches for caching:
    // https://ardalis.com/building-a-cachedrepository-in-aspnet-core/
    // https://fiseni.com/posts/alternative-caching-implementations-and-cache-invalidation/
    public class CustomerCachedRepository : ICustomerCachedRepository
    {
        private readonly IMemoryCache cache;
        private readonly ICustomerRepository customerRepository;

        private readonly string cacheKeyCustomers = "customers";
        private readonly string cacheKeyCustomerNames = "customerNames";
        private readonly TimeSpan cacheDuration = TimeSpan.FromSeconds(1);

        public CustomerCachedRepository(IMemoryCache cache,
                                        ICustomerRepository customerRepository)
        {
            this.cache = cache;
            this.customerRepository = customerRepository;
        }

        public Task<List<Customer>> GetCustomersWithContacts()
        {
            return cache.GetOrCreateAsync(cacheKeyCustomers, entry =>
            {
                entry.SlidingExpiration = cacheDuration;
                return customerRepository.GetCustomersWithContacts();
            });
        }

        public Task<List<string>> GetCustomerNames()
        {
            return cache.GetOrCreateAsync(cacheKeyCustomerNames, entry =>
            {
                entry.SlidingExpiration = cacheDuration;
                return customerRepository.GetCustomerNames();
            });
        }
    }
}
