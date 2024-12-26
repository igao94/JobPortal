using Domain.Interfaces;

namespace Persistence.Repositories;

public class UnitOfWork(DataContext context,
    IJobsRepository jobsRepository,
    IAccountsRepository accountsRepository,
    IUsersRepository usersRepository,
    IPhotosRepository photosRepository) : IUnitOfWork
{
    public IJobsRepository JobsRepository => jobsRepository;
    public IAccountsRepository AccountsRepository => accountsRepository;

    public IUsersRepository UsersRepository => usersRepository;

    public IPhotosRepository PhotosRepository => photosRepository;

    public async Task<bool> SaveAsync() => await context.SaveChangesAsync() > 0;
}
