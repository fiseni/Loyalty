using CustomerApp.Core.Entities;
using CustomerApp.Core.Enumerations;
using CustomerApp.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Infrastructure.Data
{
    // Utilized for the ADO.NET approach
    internal static class CustomerMapper
    {
        public static List<Customer> GetCustomer(List<CustomerDAO> customersDAO)
        {
            return customersDAO
                    .GroupBy(x => x.Id)
                    .Select(x => GetCustomer(x.First(), x))
                    .ToList();
        }

        private static Customer GetCustomer(CustomerDAO customerDAO, IEnumerable<CustomerDAO> contactsDAO)
        {
            var customer = new Customer
            (
                customerDAO.Id,
                (CustomerTypeEnum)customerDAO.Type,
                customerDAO.Name,
                customerDAO.Email,
                new Address(customerDAO.Street, customerDAO.City, customerDAO.PostalCode, customerDAO.Country)
            );

            foreach (var contactDAO in contactsDAO)
            {
                if (contactDAO.ContactId != null && contactDAO.ContactCustomerId != null)
                {
                    customer.AddContact(new Contact
                    (
                        contactDAO.ContactId.Value,
                        new Person(contactDAO.ContactFirstName, contactDAO.ContactLastName, contactDAO.Email, contactDAO.ContactPhone),
                        contactDAO.ContactCustomerId.Value
                    ));
                }
            }

            return customer;
        }
    }
}
