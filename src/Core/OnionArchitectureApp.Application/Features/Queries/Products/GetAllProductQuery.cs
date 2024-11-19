using MediatR;
using OnionArchitectureApp.Application.Dtos.ProductDtos;

namespace OnionArchitectureApp.Application.Features.Queries.Products;

public class GetAllProductQuery : IRequest<List<ProductGetAllDto>>
{
}
