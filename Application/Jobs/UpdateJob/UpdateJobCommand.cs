using Application.Core;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Jobs.UpdateJob;

public class UpdateJobCommand : IRequest<Result<Unit>>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}
