using CustomerApp.Api.Configuration;
using Loyalty.ApiGateway.Configuration;
using Loyalty.ApiGateway.Proxies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PozitronDev.DIConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loyalty.ApiGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var applicationOptions = ApplicationOptions.Instance;
            Configuration.Bind(ApplicationOptions.CONFIG_NAME, applicationOptions);

            if (applicationOptions.ApplicationType == ApplicationType.Monolithic)
            {
                services.AddCustomerServices(this.Configuration);
                services.AddScoped<ICustomerProxy, CustomerProxy>();
            }
            else
            {
                services.AddScoped<ICustomerProxy, CustomerApiProxy>();
            }

            // Alternatively you can bind ICustomerProxy to CustomerProxy or CustomerApiProxy on runtime, based on configuration in appsettings.json
            // https://fiseni.com/posts/open-close-principle-and-runtime-di-configuration/
            // https://github.com/fiseni/PozitronDev.DIConfiguration
            // services.AddBindings(Configuration);

            services.AddMemoryCache();

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddControllers(options =>
            {
                options.Conventions.Add(new ActionHidingConvention());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Loyalty.ApiGateway", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Loyalty.ApiGateway v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
