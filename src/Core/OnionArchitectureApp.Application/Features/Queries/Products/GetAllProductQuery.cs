using MediatR;
using OnionArchitectureApp.Application.Dtos.ProductDtos;
using OnionArchitectureApp.Application.Wrappers;

namespace OnionArchitectureApp.Application.Features.Queries.Products;

public class GetAllProductQuery : IRequest<ResponseWrapper<List<ProductGetAllDto>>>
{
}
