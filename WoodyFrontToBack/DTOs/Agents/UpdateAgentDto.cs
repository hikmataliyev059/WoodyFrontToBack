namespace WoodyFrontToBack.DTOs.Agents;

public record UpdateAgentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? ImageUrl { get; set; }
    public IFormFile? File { get; set; }
    public int? PositionId { get; set; }
}
