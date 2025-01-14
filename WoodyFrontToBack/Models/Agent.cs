using WoodyFrontToBack.Models.Common;

namespace WoodyFrontToBack.Models;

public class Agent : BaseEntity
{
    public string Name { get; set; }   
    public string? ImageUrl { get; set; }
    public IFormFile File { get; set; }
    public int PositionId { get; set; }
    public Position Position { get; set; }
}
