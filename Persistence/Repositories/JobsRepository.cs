using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class JobsRepository(DataContext context) : IJobsRepository
{
    public void AddJob(Job job) => context.Jobs.Add(job);

    public void DeleteJob(Job job) => context.Jobs.Remove(job);

    public async Task<List<Job>> GetAllJobsAsync() => await context.Jobs.ToListAsync();

    public async Task<Job?> GetJobByIdAsync(Guid id) => await context.Jobs.FindAsync(id);
}
