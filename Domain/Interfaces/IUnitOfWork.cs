namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IJobsRepository JobsRepository { get; }
    Task<bool> SaveChangesAsync();
}
