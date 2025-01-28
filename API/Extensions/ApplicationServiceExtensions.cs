using Application.Core;
using Application.Jobs.CreateJob;
using Application.Jobs.GetAllJobs;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IJobsRepository, JobsRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllJobsQuery).Assembly));

        services.AddFluentValidationAutoValidation();

        services.AddValidatorsFromAssemblyContaining<CreateJobCommand>();

        return services;
    }
}
