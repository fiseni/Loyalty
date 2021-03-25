using CustomerApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Core.Contracts
{
    // Usually you won't need separate interface ICustomerCachedRepository.
    // I have defined it just to demonstrate various usages in CustomerService.
    // Various approaches for caching:
    // https://ardalis.com/building-a-cachedrepository-in-aspnet-core/
    // https://fiseni.com/posts/alternative-caching-implementations-and-cache-invalidation/
    public interface ICustomerCachedRepository
    {
        Task<List<string>> GetCustomerNames();
        Task<List<Customer>> GetCustomersWithContacts();
    }
}
