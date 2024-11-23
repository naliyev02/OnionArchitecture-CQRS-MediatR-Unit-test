namespace OnionArchitectureApp.Application.Dtos.ProductDtos;

public class ProductGetAllDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int StockQuantity { get; set; }
    public string TypeName { get; set; }
}
