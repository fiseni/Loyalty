using CustomerApp.Core.Contracts;
using CustomerApp.Core.Entities;
using CustomerApp.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Infrastructure.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly LoyaltyDbContext dbContext;

        public CustomerRepository(LoyaltyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<List<string>> GetCustomerNames()
        {
            return dbContext.Customers.Select(x => x.Name).ToListAsync();
        }

        // The usage with EntityFramework Core directly.
        public Task<List<Customer>> GetCustomersWithContacts()
        {
            return dbContext.Customers.Include(x => x.Contacts).ToListAsync();
        }

        // Combination of EF and rawl sql.
        public Task<List<Customer>> GetCustomersWithContacts_Option2()
        {
            var result =  dbContext.Customers
                                .FromSqlRaw("select * from Customer")
                                .Include(x=>x.Contacts)
                                .ToListAsync();

            return result;
        }

        // If you're nostalgic to ADO.NET :), here is how you can do it through EntityFramework Core.
        // This is a tedious work, you'll have to map the results to separate DAO (data access object) models, and build your entities manually.
        public Task<List<Customer>> GetCustomersWithContacts_Option3()
        {
            var customersDAO = new List<CustomerDAO>();

            using (var command = dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = string.Format(
@"
SELECT * from Customer 
LEFT JOIN Contact on Customer.Id = Contact.CustomerId
");

                dbContext.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var item = new CustomerDAO();

                            item.Id = reader.GetGuid(0);
                            item.Name = reader.IsDBNull(1) ? null : reader.GetString(1);
                            item.Email = reader.IsDBNull(2) ? null : reader.GetString(2);
                            item.Type = reader.GetInt32(3);
                            item.Street = reader.IsDBNull(4) ? null : reader.GetString(4);
                            item.City = reader.IsDBNull(5) ? null : reader.GetString(5);
                            item.PostalCode = reader.IsDBNull(6) ? null : reader.GetString(6);
                            item.Country = reader.IsDBNull(7) ? null : reader.GetString(7);
                            item.ContactId = reader.IsDBNull(14) ? null : reader.GetGuid(14);
                            item.ContactFirstName = reader.IsDBNull(15) ? null : reader.GetString(15);
                            item.ContactLastName = reader.IsDBNull(16) ? null : reader.GetString(16);
                            item.ContactEmail = reader.IsDBNull(17) ? null : reader.GetString(17);
                            item.ContactPhone = reader.IsDBNull(18) ? null : reader.GetString(18);
                            item.ContactCustomerId = reader.IsDBNull(19) ? null : reader.GetGuid(19);

                            customersDAO.Add(item);
                        }
                    }
                }
            }

            return Task.FromResult(CustomerMapper.GetCustomer(customersDAO));
        }
    }
}
