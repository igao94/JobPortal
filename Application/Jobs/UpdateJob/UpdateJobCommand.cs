using Application.Core;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Jobs.UpdateJob;

public class UpdateJobCommand : IRequest<Result<Unit>>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string CompanyName { get; set; } = default!;
    public string Location { get; set; } = default!;
}
