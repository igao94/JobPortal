﻿using Application.Core;
using Application.Jobs.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Jobs.GetJobById;

public class GetJobByIdHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetJobIdQuery, Result<JobDto>?>
{
    public async Task<Result<JobDto>?> Handle(GetJobIdQuery request, CancellationToken cancellationToken)
    {
        var jobQuery = unitOfWork.JobsRepository.GetJobQueryById(request.Id);

        var job = await jobQuery.ProjectTo<JobDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync();

        if (job is null) return null;

        return Result<JobDto>.Success(job);
    }
}