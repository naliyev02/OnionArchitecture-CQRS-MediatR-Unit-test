using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnionArchitectureApp.Application.Dtos.ProductDtos;
using OnionArchitectureApp.Application.Exceptions.ProductExceptions;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Interfaces.UnitOfWork;
using OnionArchitectureApp.Application.Wrappers;
using System.Net;

namespace OnionArchitectureApp.Application.Features.Queries.Products;

public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ResponseWrapper<ProductGetByIdDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseWrapper<ProductGetByIdDto>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id, x => x.Include(x => x.Type).Include(x => x.ProductCategoryRels).ThenInclude(x => x.Category));
        if (product is null)
            throw new ProductNotFoundException(request.Id);

        var productGetByIdDto = _mapper.Map<ProductGetByIdDto>(product);

        return await ResponseWrapper<ProductGetByIdDto>.SuccessResult(productGetByIdDto, HttpStatusCode.OK);
    }
}
