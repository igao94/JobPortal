namespace Domain.Entities;

public class Photo
{
    public string PhotoId { get; set; } = default!;
    public string Url { get; set; } = default!;
    public bool IsMain { get; set; }
}
