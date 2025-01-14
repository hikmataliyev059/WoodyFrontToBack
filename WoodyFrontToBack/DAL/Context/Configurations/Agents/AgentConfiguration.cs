using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WoodyFrontToBack.Models;

namespace WoodyFrontToBack.DAL.Context.Configurations.Agents;

public class AgentConfiguration : IEntityTypeConfiguration<Agent>
{
    public void Configure(EntityTypeBuilder<Agent> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.ImageUrl)
             .IsRequired(false)
             .HasMaxLength(300);

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Position)
            .WithMany(x => x.Agents)
            .HasForeignKey(x => x.PositionId);

        builder.Ignore(x => x.File);
    }
}
