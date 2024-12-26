using Application.Core;

namespace Application.Jobs;

public class JobsParams : PagingParams
{
    public string? SearchTerm { get; set; }
    public string? SortColumn { get; set; }
    public string? SortOrder { get; set; }
}
