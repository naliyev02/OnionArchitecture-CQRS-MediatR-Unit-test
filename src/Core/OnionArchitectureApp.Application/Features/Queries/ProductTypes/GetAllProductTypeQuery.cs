using MediatR;
using OnionArchitectureApp.Application.Dtos.ProductTypeDtos;
using OnionArchitectureApp.Application.Wrappers;

namespace OnionArchitectureApp.Application.Features.Queries.ProductTypes;

public class GetAllProductTypeQuery : IRequest<ResponseWrapper<List<ProductTypeGetAllDto>>>
{
}
