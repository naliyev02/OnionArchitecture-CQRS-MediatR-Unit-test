using MediatR;
using OnionArchitectureApp.Application.Dtos.ProductDtos;

namespace OnionArchitectureApp.Application.Features.Queries.Products;

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ProductGetByIdDto>
{
    public Task<ProductGetByIdDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        ProductGetByIdDto result = new ProductGetByIdDto()
        {
            Id = Guid.NewGuid(),
            Name = "Product1"
        };

        return Task.FromResult(result);
    }
}
