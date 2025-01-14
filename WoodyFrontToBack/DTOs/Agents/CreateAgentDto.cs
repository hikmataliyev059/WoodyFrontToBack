namespace WoodyFrontToBack.DTOs.Agents;

public record CreateAgentDto
{
    public string Name { get; set; }
    public IFormFile File { get; set; }
    public string? ImageUrl { get; set; }
    public int PositionId { get; set; }
}
