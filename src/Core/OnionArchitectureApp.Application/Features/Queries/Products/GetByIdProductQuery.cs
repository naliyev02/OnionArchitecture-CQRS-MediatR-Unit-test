using MediatR;
using OnionArchitectureApp.Application.Dtos.ProductDtos;
using OnionArchitectureApp.Application.Wrappers;

namespace OnionArchitectureApp.Application.Features.Queries.Products;

public class GetByIdProductQuery : IRequest<ResponseWrapper<ProductGetByIdDto>>
{
    public Guid Id { get; set; }
    
}
