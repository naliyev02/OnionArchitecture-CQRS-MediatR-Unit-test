using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionArchitectureApp.Domain.Entities;
using OnionArchitectureApp.Persistence.Configurations.Common;

namespace OnionArchitectureApp.Persistence.Configurations;

public class ProductCategoryRelConfiguration : BaseConfiguration<ProductCategoryRel>, IEntityTypeConfiguration<ProductCategoryRel>
{
    public void Configure(EntityTypeBuilder<ProductCategoryRel> builder)
    {
        base.Configure(builder);


        builder.HasOne(x => x.Product).WithMany(x => x.ProductCategoryRels).HasForeignKey(x => x.ProductId);
        builder.HasOne(x => x.Category).WithMany(x => x.ProductCategoryRels).HasForeignKey(x => x.CategoryId);

        builder.ToTable("PRODUCT_CATEGORY_RELS");

    }
}
