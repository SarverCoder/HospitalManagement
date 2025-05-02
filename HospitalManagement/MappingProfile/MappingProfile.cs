using AutoMapper;
using HospitalManagement.DataAccess.Entities;
using HospitalManagement.Dtos;

namespace HospitalManagement.MappingProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<CreateRoomDto, Room>().ReverseMap();

        CreateMap<Room, RoomDto>().ReverseMap();

        CreateMap<UpdateRoomDto, Room>().ReverseMap();

        CreateMap<Doctor, DoctorDto>()
            .ForMember(dest => dest.FullName,
                opt =>
                    opt.MapFrom(src => $"{src.Firstname} {src.Lastname}"));


        CreateMap<Doctor, CreateDoctorDto>().ReverseMap();



        CreateMap<Patient, PatientDto>()
            .ForMember(dest => dest.FullName,
                opt 
                    => opt.MapFrom(src => $"{src.Firstname} {src.Lastname}"))
            .ForMember(p => p.DateOfBirth,
                opt 
                    => opt.MapFrom(src => src.DateOfBirth.ToString()))
            .ForMember(p => p.BlankIdentifier, opt 
                => opt.MapFrom(src => src.PatientBlank.BlankIdentifier))
            .ReverseMap();


    }
}
