using CustomerApp.Core.Entities;
using CustomerApp.Core.Enumerations;
using CustomerApp.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Infrastructure.DataAccess.Seeds
{
    public static class CustomerSeed
    {
        public static List<Customer> GetCustomers()
        {
            var customers = new List<Customer>();

            for (int i = 0; i < 50; i++)
            {
                var customer = new Customer
                (
                    i % 2 == 0 ? CustomerTypeEnum.Domestic : CustomerTypeEnum.International,
                    $"Customer-{i}",
                    $"Customer-{i}@local",
                    new Address($"Customer-{i}-Street", $"Customer-{i}-City", $"Customer-{i}-PostalCode", $"Customer-{i}-Country")
                );

                customer.AddContact(new Person($"FirstName-{i}", $"LastName-{i}", $"Email-{i}", $"Phone-{i}"));

                customers.Add(customer);
            }

            return customers;
        }
    }
}
