using AutoMapper;
using MediatR;
using OnionArchitectureApp.Application.Dtos.ProductTypeDtos;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Interfaces.UnitOfWork;
using OnionArchitectureApp.Application.Wrappers;

namespace OnionArchitectureApp.Application.Features.Queries.ProductTypes;

public class GetAllProductTypeQueryHandler : IRequestHandler<GetAllProductTypeQuery, ResponseWrapper<List<ProductTypeGetAllDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllProductTypeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseWrapper<List<ProductTypeGetAllDto>>> Handle(GetAllProductTypeQuery request, CancellationToken cancellationToken)
    {
        var productTypes = _unitOfWork.ProductTypeRepository.GetAll();
        var productTypeDtos = _mapper.Map<List<ProductTypeGetAllDto>>(productTypes);

        return await ResponseWrapper<List<ProductTypeGetAllDto>>.SuccessResult(productTypeDtos, System.Net.HttpStatusCode.OK);
    }
}
