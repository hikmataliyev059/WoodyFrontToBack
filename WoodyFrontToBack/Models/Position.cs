using WoodyFrontToBack.Models.Common;

namespace WoodyFrontToBack.Models;

public class Position : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Agent> Agents { get; set; }
}
