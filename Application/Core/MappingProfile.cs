using Application.Jobs.CreateJob;
using Application.Jobs.DTOs;
using Application.Jobs.UpdateJob;
using Application.Photos.DTOs;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Core;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Job, JobDto>();

        CreateMap<CreateJobCommand, Job>();

        CreateMap<UpdateJobCommand, Job>();

        CreateMap<JobApplication, JobApplicationDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.AppUser.UserName))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.AppUser.Photos
                .FirstOrDefault(p => p.IsMain)!.Url));

        CreateMap<AppUser, UserDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Photos
                .FirstOrDefault(p => p.IsMain)!.Url));

        CreateMap<Photo, PhotoDto>();
    }
}
