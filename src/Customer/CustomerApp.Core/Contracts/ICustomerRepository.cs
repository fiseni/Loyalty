using CustomerApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Core.Contracts
{
    public interface ICustomerRepository
    {
        Task<List<string>> GetCustomerNames();
        Task<List<Customer>> GetCustomersWithContacts();
    }
}
