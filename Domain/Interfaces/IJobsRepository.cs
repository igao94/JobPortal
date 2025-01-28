using Domain.Entities;

namespace Domain.Interfaces;

public interface IJobsRepository
{
    Task<List<Job>> GetAllJobsAsync();
    Task<Job?> GetJobByIdAsync(Guid id);
    void AddJob(Job job);
    void DeleteJob(Job job);
}
