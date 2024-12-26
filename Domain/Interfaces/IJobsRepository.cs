using Domain.Entities;

namespace Domain.Interfaces;

public interface IJobsRepository
{
    IQueryable<Job> GetAllJobsQuery(string? searchTerm, string? sortColumn, string? sortOrder);
    IQueryable<Job> GetJobQueryById(Guid id);
    Task<Job?> GetJobByIdAsync(Guid id);
    void CreateJob(Job job);
    void DeleteJob(Job job);
    Task<JobApplication?> GetJobApplicationAsync(string userId, Guid jobId);
    void AddJobApplication(JobApplication jobApplication);
}
