using Domain.Interfaces;

namespace Persistence.Repositories;

public class UnitOfWork(DataContext context,
    IJobsRepository jobsRepository,
    IAccountsRepository accountsRepository,
    IUsersRepository usersRepository,
    IAdminRepository adminRepository) : IUnitOfWork
{
    public IJobsRepository JobsRepository => jobsRepository;

    public IAccountsRepository AccountsRepository => accountsRepository;

    public IUsersRepository UsersRepository => usersRepository;

    public IAdminRepository AdminRepository => adminRepository;

    public async Task<bool> SaveChangesAsync() => await context.SaveChangesAsync() > 0;
}
