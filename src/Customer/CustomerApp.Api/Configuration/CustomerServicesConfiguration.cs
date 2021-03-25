using CustomerApp.Api.Contracts;
using CustomerApp.Api.Services;
using CustomerApp.Core.Contracts;
using CustomerApp.Infrastructure;
using CustomerApp.Infrastructure.Data;
using CustomerApp.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApp.Api.Configuration
{
    public static class CustomerServicesConfiguration
    {
        public static void AddCustomerServices(this IServiceCollection services, IConfiguration configuration)
        {
            var constring = configuration.GetConnectionString("LoyaltyDbConnection");
            services.AddDbContext<LoyaltyDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("LoyaltyDbConnection")));
            services.AddScoped<DbInitializer>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerCachedRepository, CustomerCachedRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            // Alternatively you can bind various implementations on runtime, based on configuration in appsettings.json
            // https://fiseni.com/posts/open-close-principle-and-runtime-di-configuration/
            // https://github.com/fiseni/PozitronDev.DIConfiguration
            // services.AddBindings(Configuration);

            services.AddAutoMapper(typeof(CustomerServicesConfiguration).Assembly);
        }
    }
}
