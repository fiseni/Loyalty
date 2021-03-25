using AutoMapper;
using CustomerApp.Api.Contracts;
using CustomerApp.Api.Models;
using CustomerApp.Core.Contracts;
using CustomerApp.Core.Entities;
using CustomerApp.Core.Specifications;
using CustomerApp.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApp.Api.Services
{
    // This service will be an entry point to the Customer component.
    // All available actions should be exposed through one or more services.
    // Keeping all actions consolidated in services will enable us to more easily switch and run it as separate process.
    // I'm demonstrating various approaches, you will usually choose one approach that fits you best, and inject only required constructs.
    public class CustomerService : ICustomerService
    {
        private readonly LoyaltyDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IRepository<Customer> repository;
        private readonly ICustomerRepository customerRepository;
        private readonly ICustomerCachedRepository customerCachedRepository;

        public CustomerService(LoyaltyDbContext dbContext,
                               IMapper mapper,
                               IRepository<Customer> repository,
                               ICustomerRepository customerRepository,
                               ICustomerCachedRepository customerCachedRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.repository = repository;
            this.customerRepository = customerRepository;
            this.customerCachedRepository = customerCachedRepository;
        }

        // Utilizing the custom repository
        public async Task<List<CustomerDto>> GetCustomers()
        {
            var customers = await customerRepository.GetCustomersWithContacts();

            return mapper.Map<List<CustomerDto>>(customers);
        }

        // Utilizing the cached variant of the custom repository
        public async Task<List<CustomerDto>> GetCustomers_Option2()
        {
            var customers = await customerCachedRepository.GetCustomersWithContacts();

            return mapper.Map<List<CustomerDto>>(customers);
        }

        // This is sample how you can do it with Specification pattern
        public async Task<List<CustomerDto>> GetCustomers_Option3()
        {
            var customers = await repository.ListAsync(new CustomersWithContactsSpec());

            return mapper.Map<List<CustomerDto>>(customers);
        }

        // If you wanna work directly with DbContext. If you find repositories too complicated, use it directly.
        public async Task<List<CustomerDto>> GetCustomers_Option4()
        {
            var customers = await dbContext.Customers.Include(x => x.Contacts).ToListAsync();

            return mapper.Map<List<CustomerDto>>(customers);
        }

        public Task<List<string>> GetCustomerNames()
        {
            return customerRepository.GetCustomerNames();
        }

        public Task<List<string>> GetCustomerNames_Option2()
        {
            return customerCachedRepository.GetCustomerNames();
        }

        public Task<List<string>> GetCustomerNames_Option3()
        {
            return repository.ListAsync(new CustomerNamesSpec());
        }

        public Task<List<string>> GetCustomerNames_Option4()
        {
            return dbContext.Customers.Select(x => x.Name).ToListAsync();
        }
    }
}
