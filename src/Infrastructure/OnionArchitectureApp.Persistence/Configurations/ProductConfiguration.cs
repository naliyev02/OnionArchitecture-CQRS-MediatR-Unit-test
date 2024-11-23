using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchitectureApp.Domain.Entities;
using OnionArchitectureApp.Persistence.Configurations.Common;

namespace OnionArchitectureApp.Persistence.Configurations;

public class ProductConfiguration : BaseConfiguration<Product>, IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.BrandName).HasMaxLength(100);

        builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
        builder.HasCheckConstraint("CK_Product_Price", "[Price] >= 0");

        builder.Property(x => x.StockQuantity).IsRequired();
        builder.HasCheckConstraint("CK_Product_StockQuantity", "[StockQuantity] >= 0");

        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        builder.HasOne(x => x.Type).WithMany(x => x.Products).HasForeignKey(x => x.TypeId);

        builder.ToTable("PRODUCTS");
    }
}
