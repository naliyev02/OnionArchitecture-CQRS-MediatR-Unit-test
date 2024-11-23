using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureApp.Application.Dtos.ProductDtos;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Wrappers;
using System.Net;

namespace OnionArchitectureApp.Application.Features.Queries.Products;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, ResponseWrapper<List<ProductGetAllDto>>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetAllProductQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseWrapper<List<ProductGetAllDto>>> Handle(GetAllProductQuery query, CancellationToken cancellationToken)
    {
        var products = _repository.GetAll(x => x.Include(x => x.Type));

        var productGetAllDto = _mapper.Map<List<ProductGetAllDto>>(products);

        return await ResponseWrapper<List<ProductGetAllDto>>.SuccessResult(productGetAllDto, HttpStatusCode.OK);
    }
}
