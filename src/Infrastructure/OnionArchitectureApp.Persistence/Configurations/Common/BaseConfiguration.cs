using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchitectureApp.Domain.Common;

namespace OnionArchitectureApp.Persistence.Configurations.Common;

public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        BaseConfigure(builder);
    }

    protected void BaseConfigure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);
    }
}
