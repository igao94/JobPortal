namespace Application.Jobs.DTOs;

public record JobDto(Guid Id,
    string Title,
    string Description,
    string CompanyName,
    string Location,
    DateTime PostedDate,
    ICollection<JobApplicationDto> Applicants);
