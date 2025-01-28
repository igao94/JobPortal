using Domain.Entities;

namespace Persistence;

public class Seed
{
    public static async Task SeedDataAsync(DataContext context)
    {
        if (!context.Jobs.Any())
        {
            List<Job> jobs =
            [
                new()
                {
                    Title = "Software Developer",
                    CompanyName = "TechCorp Inc.",
                    Description = "Develop and maintain web applications using .NET Core.",
                    Location = "New York, NY",
                    PostedDate = DateTime.UtcNow.AddDays(-3)
                },

                new()
                {
                    Title = "Frontend Developer",
                    CompanyName = "WebSolutions Ltd.",
                    Description = "Build responsive and interactive user interfaces using JavaScript and React.",
                    Location = "San Francisco, CA",
                    PostedDate = DateTime.UtcNow.AddDays(-4)
                },

                new()
                {
                    Title = "Backend Developer",
                    CompanyName = "DataTech Solutions",
                    Description = "Design and implement backend services using .NET and SQL Server.",
                    Location = "Austin, TX",
                    PostedDate = DateTime.UtcNow.AddDays(-5)
                },

                new()
                {
                    Title = "Mobile App Developer",
                    CompanyName = "AppVenture Studios",
                    Description = "Develop cross-platform mobile applications using Xamarin and .NET.",
                    Location = "Chicago, IL",
                    PostedDate = DateTime.UtcNow.AddDays(-7)
                },

                new()
                {
                    Title = "Cloud Engineer",
                    CompanyName = "CloudNet Technologies",
                    Description = "Manage and deploy cloud infrastructure on AWS and Azure.",
                    Location = "Seattle, WA",
                    PostedDate = DateTime.UtcNow.AddDays(-10)
                },

                new()
                {
                    Title = "DevOps Engineer",
                    CompanyName = "DevOps Solutions",
                    Description = "Automate deployment pipelines and monitor system performance.",
                    Location = "Los Angeles, CA",
                    PostedDate = DateTime.UtcNow.AddDays(-2)
                },

                new()
                {
                    Title = "UI/UX Designer",
                    CompanyName = "Design Studios",
                    Description = "Create intuitive user interfaces and enhance user experiences.",
                    Location = "Boston, MA",
                    PostedDate = DateTime.UtcNow.AddDays(-12)
                },

                new()
                {
                    Title = "Data Scientist",
                    CompanyName = "Analytics Corp.",
                    Description = "Analyze large data sets to generate actionable insights and predictions.",
                    Location = "San Francisco, CA",
                    PostedDate = DateTime.UtcNow.AddDays(-20)
                },

                new()
                {
                    Title = "Full Stack Developer",
                    CompanyName = "CodeWorks Inc.",
                    Description = "Develop end-to-end solutions using .NET Core and Angular.",
                    Location = "Denver, CO",
                    PostedDate = DateTime.UtcNow.AddDays(-8)
                },

                new()
                {
                    Title = "Systems Analyst",
                    CompanyName = "Tech Innovators",
                    Description = "Analyze system requirements and propose solutions for business needs.",
                    Location = "Dallas, TX",
                    PostedDate = DateTime.UtcNow.AddDays(-15)
                }
            ];

            context.Jobs.AddRange(jobs);

            await context.SaveChangesAsync();
        }
    }
}
