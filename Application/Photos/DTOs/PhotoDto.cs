namespace Application.Photos.DTOs;

public class PhotoDto
{
    public string PhotoId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsMain { get; set; }
}
