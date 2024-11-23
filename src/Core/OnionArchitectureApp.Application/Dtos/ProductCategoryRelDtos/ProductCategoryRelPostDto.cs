using OnionArchitectureApp.Domain.Entities;

namespace OnionArchitectureApp.Application.Dtos.ProductCategoryRelDtos;

public class ProductCategoryRelPostDto
{
    public Guid? ProductId { get; set; }
    public Guid? CategoryId { get; set; }
}
