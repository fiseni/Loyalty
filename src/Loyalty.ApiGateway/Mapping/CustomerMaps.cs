using AutoMapper;
using CustomerApp.Api.Models;
using Loyalty.ApiGateway.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loyalty.ApiGateway.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto, CustomerResult>();

            CreateMap<ContactDto, ContactResult>();
        }
    }
}
