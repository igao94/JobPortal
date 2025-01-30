using Domain.Entities;
using Domain.Interfaces;

namespace Persistence.Repositories;

public class JobsRepository(DataContext context) : IJobsRepository
{
    public void AddJob(Job job) => context.Jobs.Add(job);

    public void DeleteJob(Job job) => context.Jobs.Remove(job);

    public IQueryable<Job> GetAllJobsQuery() => context.Jobs.AsQueryable();

    public IQueryable<Job> GetJobByIdQuery(Guid id) => context.Jobs.Where(j => j.Id == id).AsQueryable();

    public async Task<Job?> GetJobByIdAsync(Guid id) => await context.Jobs.FindAsync(id);

    public async Task<JobApplication?> GetJobApplicationByIdAsync(string userId, Guid jobId)
    {
        return await context.JobApplications.FindAsync(userId, jobId);
    }

    public void AddJobApplication(JobApplication jobApplication) => context.JobApplications.Add(jobApplication);
}
