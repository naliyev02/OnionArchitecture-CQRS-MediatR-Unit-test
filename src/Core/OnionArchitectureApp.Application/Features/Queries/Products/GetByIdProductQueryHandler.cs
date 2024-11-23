using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureApp.Application.Dtos.ProductDtos;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Wrappers;
using System.Net;

namespace OnionArchitectureApp.Application.Features.Queries.Products;

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ResponseWrapper<ProductGetByIdDto>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetByIdProductQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseWrapper<ProductGetByIdDto>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, x => x.Include(x => x.Type).Include(x => x.ProductCategoryRels).ThenInclude(x => x.Category));

        var productGetByIdDto = _mapper.Map<ProductGetByIdDto>(product);

        return await ResponseWrapper<ProductGetByIdDto>.SuccessResult(productGetByIdDto, HttpStatusCode.OK);
    }
}
