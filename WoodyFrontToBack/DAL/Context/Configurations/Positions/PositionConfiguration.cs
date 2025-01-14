using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WoodyFrontToBack.Models;

namespace WoodyFrontToBack.DAL.Context.Configurations.Positions;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(30);
    }
}
