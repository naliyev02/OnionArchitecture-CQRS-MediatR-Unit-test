using AutoMapper;
using MediatR;
using OnionArchitectureApp.Application.Dtos.ProductTypeDtos;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Wrappers;

namespace OnionArchitectureApp.Application.Features.Queries.ProductTypes;

public class GetAllProductTypeQueryHandler : IRequestHandler<GetAllProductTypeQuery, ResponseWrapper<List<ProductTypeGetAllDto>>>
{
    private readonly IProductTypeRepository _repository;
    private readonly IMapper _mapper;

    public GetAllProductTypeQueryHandler(IProductTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseWrapper<List<ProductTypeGetAllDto>>> Handle(GetAllProductTypeQuery request, CancellationToken cancellationToken)
    {
        var productTypes = _repository.GetAll();
        var productTypeDtos = _mapper.Map<List<ProductTypeGetAllDto>>(productTypes);

        return await ResponseWrapper<List<ProductTypeGetAllDto>>.SuccessResult(productTypeDtos, System.Net.HttpStatusCode.OK);
    }
}
