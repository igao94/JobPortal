using Domain.Interfaces;

namespace Persistence.Repositories;

public class UnitOfWork(DataContext context,
    IJobsRepository jobsRepository) : IUnitOfWork
{
    public IJobsRepository JobsRepository => jobsRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
