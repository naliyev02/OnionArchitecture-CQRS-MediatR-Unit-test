using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchitectureApp.Domain.Entities;
using OnionArchitectureApp.Persistence.Configurations.Common;

namespace OnionArchitectureApp.Persistence.Configurations;

public class ProductCategoryConfiguration : BaseConfiguration<ProductCategory> ,IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);

        builder.ToTable("PRODUCT_CATEGORIES");
    }
}
