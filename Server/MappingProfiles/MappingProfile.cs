using codesome.Shared.Models.DTOs;
using codesome.Shared.Models;
using AutoMapper;
using codesome.Shared.Models.DTOs.requests;
using codesome.Shared.Models.DTOs.responses;

namespace codesome.Server.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseRequestDTO>();
            CreateMap<CourseRequestDTO, Course>();
            CreateMap<Course, CourseResponseDTO>();
            CreateMap<CourseResponseDTO, Course>();
            CreateMap<CustomUser, UserResponseDTO>();
            CreateMap<UserResponseDTO, CustomUser>();
            CreateMap<CustomUser, UserRequestDTO>();
            CreateMap<UserRequestDTO, CustomUser>();

            CreateMap<Enrollment, EnrollmentResponseDTO>();
            CreateMap<EnrollmentResponseDTO, Enrollment>();
            CreateMap<Module, ModuleResponseDTO>();
        }
    }
}
