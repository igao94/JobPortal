using Application.Jobs.CreateJob;
using Application.Jobs.DTOs;
using Application.Jobs.UpdateJob;
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
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.AppUser.UserName));
    }
}
