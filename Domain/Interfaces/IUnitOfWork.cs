namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IJobsRepository JobsRepository { get; }
    IAccountsRepository AccountsRepository { get; }
    Task<bool> SaveChangesAsync();
}
