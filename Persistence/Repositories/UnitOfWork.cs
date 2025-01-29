using Domain.Interfaces;

namespace Persistence.Repositories;

public class UnitOfWork(DataContext context,
    IJobsRepository jobsRepository,
    IAccountsRepository accountsRepository) : IUnitOfWork
{
    public IJobsRepository JobsRepository => jobsRepository;

    public IAccountsRepository AccountsRepository => accountsRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
