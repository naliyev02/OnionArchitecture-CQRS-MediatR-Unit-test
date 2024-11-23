using OnionArchitectureApp.Domain.Common;

namespace OnionArchitectureApp.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? BrandName { get; set; }
    public double Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }

    public Guid? TypeId { get; set; }
    public ProductType? Type { get; set; }

    public ICollection<ProductCategoryRel>? ProductCategoryRels { get; set; }
}
