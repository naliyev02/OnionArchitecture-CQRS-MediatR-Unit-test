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

    private void EnsureValidPrice(double price)
    {
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero.");
    }

    public void UpdatePrice(double price)
    {
        EnsureValidPrice(price);
        this.Price = price;
    }

    private void EnsureValidStockQuantity(int stockQuantity)
    {
        if (stockQuantity <= 0)
            throw new ArgumentException("Stock quantity must be greater than zero.");
    }

    public void UpdateStockQuantity(int stockQuantity)
    {
        EnsureValidStockQuantity(stockQuantity);
        this.StockQuantity = stockQuantity;
    }

    public void Validate()
    {
        EnsureValidPrice(this.Price);
        EnsureValidStockQuantity(this.StockQuantity);
    }

}
