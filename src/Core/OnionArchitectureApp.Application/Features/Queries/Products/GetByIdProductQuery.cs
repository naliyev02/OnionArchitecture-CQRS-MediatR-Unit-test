using MediatR;
using OnionArchitectureApp.Application.Dtos.ProductDtos;

namespace OnionArchitectureApp.Application.Features.Queries.Products;

public class GetByIdProductQuery : IRequest<ProductGetByIdDto>
{
    public Guid Id { get; set; }
}
