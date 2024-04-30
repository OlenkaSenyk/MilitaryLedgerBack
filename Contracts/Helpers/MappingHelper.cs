using Contracts.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using File = Domain.Entities.File;

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

            CreateMap<Award, AwardDTO>();
            CreateMap<Award, AwardDTO>().ReverseMap();
            CreateMap<Award, AwardForAddingDTO>();
            CreateMap<Award, AwardForAddingDTO>().ReverseMap();

            CreateMap<CombatParticipation, CombatParticipationDTO>();
            CreateMap<CombatParticipation, CombatParticipationDTO>().ReverseMap();
            CreateMap<CombatParticipation, CombatParticipationForAddingDTO>();
            CreateMap<CombatParticipation, CombatParticipationForAddingDTO>().ReverseMap();

            CreateMap<ServiceHistory, ServiceHistoryDTO>();
            CreateMap<ServiceHistory, ServiceHistoryDTO>().ReverseMap();
            CreateMap<ServiceHistory, ServiceHistoryForAddingDTO>();
            CreateMap<ServiceHistory, ServiceHistoryForAddingDTO>().ReverseMap();

            CreateMap<Injurie, InjurieDTO>();
            CreateMap<Injurie, InjurieDTO>().ReverseMap();
            CreateMap<Injurie, InjurieForAddingDTO>();
            CreateMap<Injurie, InjurieForAddingDTO>().ReverseMap();

            CreateMap<Parameter, ParameterDTO>();
            CreateMap<Parameter, ParameterDTO>().ReverseMap();
            CreateMap<Parameter, ParameterForAddingDTO>();
            CreateMap<Parameter, ParameterForAddingDTO>().ReverseMap();

            CreateMap<MedicalData, MedicalDataDTO>();
            CreateMap<MedicalData, MedicalDataDTO>().ReverseMap();
            CreateMap<MedicalData, MedicalDataForAddingDTO>();
            CreateMap<MedicalData, MedicalDataForAddingDTO>().ReverseMap();

            CreateMap<File, FileDTO>();
            CreateMap<File, FileDTO>().ReverseMap();
            CreateMap<File, FileForAddingDTO>();
            CreateMap<File, FileForAddingDTO>().ReverseMap();

            CreateMap<RegistrationRequestDTO, User>()
                .ForMember(u => u.Password, opt => opt.MapFrom(src => SecurityHelper.GetHashedString(src.Password)));
            CreateMap<RegistrationRequestDTO, User>().ForMember(u => u.Password, opt => opt.MapFrom(src => SecurityHelper.GetHashedString(src.Password))).ReverseMap();
            CreateMap<LoginResponseDTO, User>();
            CreateMap<LoginResponseDTO, User>().ReverseMap();
        }
    }
}
