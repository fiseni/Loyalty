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
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Person, ContactDto>();

            CreateMap<Contact, ContactDto>()
                .IncludeMembers(x => x.Details);

            CreateMap<ContactDto, ContactResult>()
                .ReverseMap();
        }
    }
}
