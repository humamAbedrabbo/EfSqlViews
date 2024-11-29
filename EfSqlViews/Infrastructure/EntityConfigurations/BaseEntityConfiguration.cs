using EfSqlViews.ApplicationCore.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfSqlViews.Infrastructure.EntityConfigurations;

internal class BaseEntityConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity<TId>
    where TId : struct
{
    protected int columnIndex = 0;

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnOrder(columnIndex++);

    }

    public virtual void ConfigureAudit(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.CreatedOn)
            .HasDefaultValueSql("SYSDATETIMEOFFSET()")
            .ValueGeneratedOnAdd()
            .HasColumnOrder(columnIndex++);

        builder.Property(e => e.LastUpdated)
            .IsRequired()
            .HasDefaultValueSql("SYSDATETIMEOFFSET()")
            .HasColumnOrder(columnIndex++);

        builder.Property(e => e.LastUpdatedBy)
            .HasMaxLength(100)
            .HasColumnOrder(columnIndex++);
    }

    public virtual void ConfigureLast(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasIndex(p => p.CreatedOn);
    }
}
