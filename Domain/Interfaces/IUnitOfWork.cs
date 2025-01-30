namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IJobsRepository JobsRepository { get; }
    IAccountsRepository AccountsRepository { get; }
    IUsersRepository UsersRepository { get; }
    Task<bool> SaveChangesAsync();
}
