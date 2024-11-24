using OnionArchitectureApp.Application.Dtos.ProductCategoryDtos;

namespace OnionArchitectureApp.Application.Dtos.ProductDtos;

public class ProductGetByIdDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? BrandName { get; set; }
    public double Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }
    public Guid TypeId { get; set; }
    public string TypeName { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<ProductCategoryGetAllDto>? Categories { get; set; }
}
