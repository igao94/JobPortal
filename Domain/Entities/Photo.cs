namespace Domain.Entities;

public class Photo
{
    public string PhotoId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsMain { get; set; }
}
