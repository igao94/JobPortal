using Application.Core;
using Application.Interfaces;
using Application.Jobs.CreateJob;
using Application.Jobs.GetAllJobs;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Repositories;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddControllers(opt =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            opt.Filters.Add(new AuthorizeFilter(policy));
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("JobPortalBearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Description = "Input a valid token to access this API."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "JobPortalBearerAuth"
                        }
                    }, []
                }
            });
        });

        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IJobsRepository, JobsRepository>();

        services.AddScoped<IAccountsRepository, AccountsRepository>();

        services.AddScoped<IUsersRepository, UsersRepository>();

        services.AddScoped<IAdminRepository, AdminRepository>();

        services.AddScoped<IPhotosRepository, PhotosRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddHttpContextAccessor();

        services.AddScoped<IUserAccessor, UserAccessor>();

        services.AddScoped<IPhotoService, PhotoService>();

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllJobsQuery).Assembly));

        services.AddFluentValidationAutoValidation();

        services.AddValidatorsFromAssemblyContaining<CreateJobCommand>();

        return services;
    }
}
