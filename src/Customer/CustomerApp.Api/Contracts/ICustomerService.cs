using CustomerApp.Api.Models;
using CustomerApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApp.Api.Contracts
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetCustomers();
        Task<List<string>> GetCustomerNames();
    }
}
