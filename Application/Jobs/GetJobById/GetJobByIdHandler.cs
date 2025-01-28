using Application.Core;
using Application.Jobs.DTOs;
using AutoMapper;
using Domain.Interfaces;
using MediatR;

namespace Application.Jobs.GetJobById;

public class GetJobByIdHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetJobByIdQuery, Result<JobDto>?>
{
    public async Task<Result<JobDto>?> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        var job = await unitOfWork.JobsRepository.GetJobByIdAsync(request.Id);

        if (job is null) return null;

        return Result<JobDto>.Success(mapper.Map<JobDto>(job));
    }
}
