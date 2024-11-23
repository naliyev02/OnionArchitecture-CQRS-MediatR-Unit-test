using OnionArchitectureApp.Domain.Common;

namespace OnionArchitectureApp.Domain.Entities;

public class ProductCategoryRel : BaseEntity
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public Guid CategoryId { get; set; }
    public ProductCategory Category { get; set; }
}
