using CustomerApp.Infrastructure.DataAccess;
using CustomerApp.Infrastructure.DataAccess.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Infrastructure
{
    public class DbInitializer
    {
        private readonly LoyaltyDbContext dbContext;
        private readonly ILogger<DbInitializer> log;

        public DbInitializer(LoyaltyDbContext dbContext,
                               ILogger<DbInitializer> logger)
        {
            this.dbContext = dbContext;
            this.log = logger;
        }

        public async Task SeedAsync(int retry = 0)
        {
            try
            {
                dbContext.Database.Migrate();

                if (await dbContext.Customers.CountAsync() == 0)
                {
                    dbContext.Customers.AddRange(CustomerSeed.GetCustomers());

                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                log.LogError("Error Occured while migrating/seeding.", ex);

                if (retry > 0)
                {
                    log.LogError("Retrying");
                    await SeedAsync(retry - 1);
                }
            }
        }
    }
}
