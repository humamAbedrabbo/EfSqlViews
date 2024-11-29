using EfSqlViews.ApplicationCore.Domain.Features.Sites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfSqlViews.Infrastructure.EntityConfigurations;
internal class SiteEntityConfiguration : BaseEntityConfiguration<Site, Guid>
{
    public override void Configure(EntityTypeBuilder<Site> builder)
    {
        builder.ToTable("Sites");

        base.Configure(builder);

        builder.Property(p => p.Name)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnOrder(columnIndex++);

        base.ConfigureAudit(builder);
        base.ConfigureLast(builder);

        builder.HasIndex(p => p.Name);
        builder.HasIndex(p => p.CreatedOn);
        builder.HasIndex(p => p.LastUpdated);
    }
}
