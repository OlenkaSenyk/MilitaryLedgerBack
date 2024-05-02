using Contracts.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using File = Domain.Entities.File;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Contracts.Helpers
{
    public class MappingHelper : Profile 
    {
        public MappingHelper()
        {
            CreateMap<AddressForAddingDTO, AddressDTO>();

            CreateMap<Award, AwardDTO>();
            CreateMap<AwardForAddingDTO, Award>();

            CreateMap<CombatParticipationForAddingDTO, CombatParticipationDTO>();

            CreateMap<FileForAddingDTO, FileDTO>()
                .ForMember(s => s.Photo, opt => opt.MapFrom(src => Convert.ToBase64String(FilesHelper.GetImageBytes(src.Photo))))
                .ForMember(s => s.Passport, opt => opt.MapFrom(src => Convert.ToBase64String(FilesHelper.GetImageBytes(src.Passport))))
                .ForMember(s => s.IndividualTaxNumber, opt => opt.MapFrom(src => Convert.ToBase64String(FilesHelper.GetImageBytes(src.IndividualTaxNumber))))
                .ForMember(s => s.MedicalCertificate, opt => opt.MapFrom(src => Convert.ToBase64String(FilesHelper.GetImageBytes(src.MedicalCertificate))))
                .ForMember(s => s.ResidencePermit, opt => opt.MapFrom(src => Convert.ToBase64String(FilesHelper.GetImageBytes(src.ResidencePermit))));

            CreateMap<MedicalDataForAddingDTO, MedicalDataDTO>();

            CreateMap<InjurieForAddingDTO, InjurieDTO>();

            CreateMap<ServiceHistoryForAddingDTO, ServiceHistoryDTO>();

            CreateMap<PersonForAddingDTO, Person>()
                .ForMember(s => s.Signature, opt => opt
                .MapFrom(src => Convert.ToBase64String(FilesHelper.GetImageBytes(src.Signature))));
            CreateMap<PersonForAddingDTO, PersonDTO>()
                .ForMember(s => s.Signature, opt => opt
                .MapFrom(src => Convert.ToBase64String(FilesHelper.GetImageBytes(src.Signature))));
            CreateMap<Person, PersonDTO>()
                .ForMember(a => a.DateOfBirth, opt => opt.Ignore());
            CreateMap<PersonDTO, Person>();

            CreateMap<Parameter, ParameterDTO>();
            CreateMap<ParameterForAddingDTO, Parameter>();

            CreateMap<User, RegistrationRequestDTO>();
        }
    }
}
