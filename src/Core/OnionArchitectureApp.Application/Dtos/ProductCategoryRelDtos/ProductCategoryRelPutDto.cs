namespace OnionArchitectureApp.Application.Dtos.ProductCategoryRelDtos;

public class ProductCategoryRelPutDto
{
    public Guid Id { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? CategoryId { get; set; }
}
