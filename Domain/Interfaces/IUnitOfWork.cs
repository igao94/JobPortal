namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IJobsRepository JobsRepository { get; }
    IAccountsRepository AccountsRepository { get; }
    IUsersRepository UsersRepository { get; }
    IPhotosRepository PhotosRepository { get; }
    Task<bool> SaveAsync();
}
