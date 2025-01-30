namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IJobsRepository JobsRepository { get; }
    IAccountsRepository AccountsRepository { get; }
    IUsersRepository UsersRepository { get; }
    IAdminRepository AdminRepository { get; }
    Task<bool> SaveChangesAsync();
}
