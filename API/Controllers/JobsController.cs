using Application.Jobs;
using Application.Jobs.ApplicationProcessing;
using Application.Jobs.ApplyForJob;
using Application.Jobs.CreateJob;
using Application.Jobs.DeleteJob;
using Application.Jobs.GetAllJobs;
using Application.Jobs.GetJobById;
using Application.Jobs.UpdateJob;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Authorization.Constants;

namespace API.Controllers;

public class JobsController(IMediator mediator) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllJobs([FromQuery] JobsParams jobsParams)
    {
        return HandlePagedResult(await mediator.Send(new GetAllJobsQuery(jobsParams)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobById(Guid id)
    {
        return HandleResult(await mediator.Send(new GetJobByIdQuery(id)));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPost]
    public async Task<IActionResult> CreateJob(CreateJobCommand command)
    {
        var result = await mediator.Send(command);

        return HandleResult(result, nameof(GetJobById), new { id = result.Value?.Id });
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJob(Guid id, UpdateJobCommand command)
    {
        command.Id = id;

        return HandleResult(await mediator.Send(command));
    }

    [Authorize(Policy = PolicyTypes.RequireAdminRole)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJob(Guid id)
    {
        return HandleResult(await mediator.Send(new DeleteJobCommand(id)));
    }

    [HttpPost("{jobId}/applyForJob")]
    public async Task<IActionResult> ApplyForJob(Guid jobId)
    {
        return HandleResult(await mediator.Send(new ApplyForJobCommand(jobId)));
    }

    [HttpPut("status")]
    public async Task<IActionResult> UpdateApplicationStatus(ApplicationProcessingCommand command)
    {
        return HandleResult(await mediator.Send(command));
    }
}
