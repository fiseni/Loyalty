using AutoMapper;
using CustomerApp.Api.Endpoints.Customer;
using CustomerApp.Api.Models;
using CustomerApp.Core.Entities;
using CustomerApp.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApp.Api.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Address, CustomerDto>();

            CreateMap<Customer, CustomerDto>()
                .IncludeMembers(x => x.Address)
                .ForMember(dto => dto.Type, options => options.MapFrom(src => src.Type.ToString()));

            CreateMap<CustomerDto, CustomerResult>()
                .ReverseMap();
        }
    }
}
