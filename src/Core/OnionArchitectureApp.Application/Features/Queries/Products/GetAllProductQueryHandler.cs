using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureApp.Application.Dtos.ProductDtos;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Interfaces.UnitOfWork;
using OnionArchitectureApp.Application.Wrappers;
using System.Net;

namespace OnionArchitectureApp.Application.Features.Queries.Products;

public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, ResponseWrapper<List<ProductGetAllDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseWrapper<List<ProductGetAllDto>>> Handle(GetAllProductQuery query, CancellationToken cancellationToken)
    {
        var products = _unitOfWork.ProductRepository.GetAll(x => x.Include(x => x.Type));

        var productGetAllDto = _mapper.Map<List<ProductGetAllDto>>(products);

        return await ResponseWrapper<List<ProductGetAllDto>>.SuccessResult(productGetAllDto, HttpStatusCode.OK);
    }
}
