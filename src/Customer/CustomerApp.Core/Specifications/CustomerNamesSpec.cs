using Ardalis.Specification;
using CustomerApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Core.Specifications
{
    public class CustomerNamesSpec : Specification<Customer, string>
    {
        public CustomerNamesSpec()
        {
            Query.Select(x => x.Name);
        }
    }
}
