using MediatR;
using OnionArchitectureApp.Application.Dtos.ProductDtos;

namespace OnionArchitectureApp.Application.Features.Queries.Products;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<ProductGetAllDto>>
{
    public Task<List<ProductGetAllDto>> Handle(GetAllProductQuery query, CancellationToken cancellationToken)
    {
        var model = new List<ProductGetAllDto>()
            {
                new ProductGetAllDto()
                {
                    Id = Guid.NewGuid(),
                }
            };

        return Task.FromResult(model);
    }
}
