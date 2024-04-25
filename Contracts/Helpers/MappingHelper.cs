using Contracts.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Contracts.Helpers
{
    public class MappingHelper : Profile 
    {
        public MappingHelper()
        {
            CreateMap<Person, PersonDTO>();
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Person, PersonForAddingDTO>();
            CreateMap<Person, PersonForAddingDTO>().ReverseMap();

            CreateMap<Person, PersonDTO>()
                .ForMember(a => a.Addresses, opt => opt.MapFrom(src => src.Addresses));
            CreateMap<PersonForAddingDTO, Person>()
                .ForMember(s => s.Signature, opt => opt.MapFrom(src => FilesHelper.GetImageBytes(src.Signature)));

            CreateMap<Address, AddressDTO>();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Address, AddressForAddingDTO>();
            CreateMap<Address, AddressForAddingDTO>().ReverseMap();
        }
    }
}
