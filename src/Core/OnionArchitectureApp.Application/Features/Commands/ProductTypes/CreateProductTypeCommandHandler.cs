using AutoMapper;
using MediatR;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Interfaces.UnitOfWork;
using OnionArchitectureApp.Application.Wrappers;
using OnionArchitectureApp.Domain.Entities;

namespace OnionArchitectureApp.Application.Features.Commands.ProductTypes;

public class CreateProductTypeCommandHandler : IRequestHandler<CreateProductTypeCommand, ResponseWrapper<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseWrapper<Guid>> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<ProductType>(request);
        await _unitOfWork.ProductTypeRepository.CreateAsync(product);

        await _unitOfWork.CompleteAsync();

        return await ResponseWrapper<Guid>.SuccessResult(product.Id, System.Net.HttpStatusCode.Created);
    }
}
