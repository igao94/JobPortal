using Application.Jobs.ApplicationProcessing;
using Application.Jobs.ApplyForJob;
using Application.Jobs.CreateJob;
using Application.Jobs.DeleteJob;
using Application.Jobs.GetAllJobs;
using Application.Jobs.GetJobById;
using Application.Jobs.UpdateJob;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class JobsController(IMediator mediator) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAllJobs()
    {
        return HandleResult(await mediator.Send(new GetAllJobsQuery()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobById(Guid id)
    {
        return HandleResult(await mediator.Send(new GetJobByIdQuery(id)));
    }

    [HttpPost]
    public async Task<IActionResult> CreateJob(CreateJobCommand command)
    {
        var result = await mediator.Send(command);

        return HandleResult(result, nameof(GetJobById), new { id = result.Value?.Id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJob(Guid id, UpdateJobCommand command)
    {
        command.Id = id;

        return HandleResult(await mediator.Send(command));
    }

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
