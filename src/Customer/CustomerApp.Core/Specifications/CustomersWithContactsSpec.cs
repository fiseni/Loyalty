using Ardalis.Specification;
using CustomerApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Core.Specifications
{
    public class CustomersWithContactsSpec : Specification<Customer>
    {
        public CustomersWithContactsSpec()
        {
            Query.Include(x => x.Contacts);
        }
    }
}
