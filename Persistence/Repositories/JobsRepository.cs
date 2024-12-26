using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class JobsRepository(DataContext context) : IJobsRepository
{
    public IQueryable<Job> GetAllJobsQuery(string? searchTerm,
        string? sortColumn,
        string? sortOrder)
    {
        IQueryable<Job> query = context.Jobs.OrderBy(j => j.PostedDate);

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(j => j.Title.Contains(searchTerm)
            || j.CompanyName.Contains(searchTerm)
            || j.Location.Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(sortColumn))
        {
            sortOrder = sortOrder?.ToLower() == "desc" ? "desc" : "asc";

            query = sortColumn.ToLower() switch
            {
                "title" => sortOrder == "asc"
                    ? query.OrderBy(j => j.Title)
                    : query.OrderByDescending(j => j.Title),
                "companyName" => sortOrder == "asc"
                    ? query.OrderBy(j => j.CompanyName)
                    : query.OrderByDescending(j => j.CompanyName),
                "location" => sortOrder == "asc"
                    ? query.OrderBy(j => j.Location)
                    : query.OrderByDescending(j => j.Location),
                _ => query
            };
        }

        return query;
    }

    public IQueryable<Job> GetJobQueryById(Guid id)
    {
        return context.Jobs.Where(j => j.Id == id).AsQueryable();
    }

    public async Task<Job?> GetJobByIdAsync(Guid id) => await context.Jobs.FirstOrDefaultAsync(u => u.Id == id);

    public void CreateJob(Job job) => context.Jobs.Add(job);

    public void DeleteJob(Job job) => context.Jobs.Remove(job);

    public async Task<JobApplication?> GetJobApplicationAsync(string userId, Guid jobId)
    {
        return await context.JobApplications.FindAsync(userId, jobId);
    }

    public void AddJobApplication(JobApplication jobApplication) => context.JobApplications.Add(jobApplication);
}
