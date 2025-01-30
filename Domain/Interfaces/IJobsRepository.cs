using Domain.Entities;

namespace Domain.Interfaces;

public interface IJobsRepository
{
    IQueryable<Job> GetAllJobsQuery();
    IQueryable<Job> GetJobByIdQuery(Guid id);
    Task<Job?> GetJobByIdAsync(Guid id);
    void AddJob(Job job);
    void DeleteJob(Job job);
    Task<JobApplication?> GetJobApplicationByIdAsync(string userId, Guid jobId);
    void AddJobApplication(JobApplication jobApplication);
}
