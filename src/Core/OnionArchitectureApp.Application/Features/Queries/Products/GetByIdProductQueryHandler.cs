using MediatR;
using OnionArchitectureApp.Application.Dtos.ProductDtos;
using OnionArchitectureApp.Application.Wrappers;
using System.Net;

namespace OnionArchitectureApp.Application.Features.Queries.Products;

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ResponseWrapper<ProductGetByIdDto>>
{
    public Task<ResponseWrapper<ProductGetByIdDto>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        ProductGetByIdDto product = new ProductGetByIdDto()
        {
            Id = Guid.NewGuid(),
            Name = "Product1"
        };

        return ResponseWrapper<ProductGetByIdDto>.SuccessResult(product, HttpStatusCode.OK);
    }
}
