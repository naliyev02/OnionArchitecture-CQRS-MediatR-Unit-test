using MediatR;
using OnionArchitectureApp.Application.Dtos.ProductCategoryRelDtos;
using OnionArchitectureApp.Application.Wrappers;
using OnionArchitectureApp.Domain.Entities;

namespace OnionArchitectureApp.Application.Features.Commands.Products;

public class UpdateProductCommand : IRequest<ResponseWrapper<Guid>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? BrandName { get; set; }
    public double? Price { get; set; }
    public int? StockQuantity { get; set; }
    public bool? IsActive { get; set; }
    public Guid? TypeId { get; set; }
    public ICollection<ProductCategoryRelPutDto>? ProductCategoryRels { get; set; }
}
