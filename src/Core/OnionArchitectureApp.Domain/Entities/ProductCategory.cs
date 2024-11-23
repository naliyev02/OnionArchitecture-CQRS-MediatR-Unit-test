using OnionArchitectureApp.Domain.Common;

namespace OnionArchitectureApp.Domain.Entities;

public class ProductCategory : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<ProductCategoryRel>? ProductCategoryRels { get; set; }
}
