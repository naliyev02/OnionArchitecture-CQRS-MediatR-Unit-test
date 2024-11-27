using AutoMapper;
using MediatR;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Interfaces.UnitOfWork;
using OnionArchitectureApp.Application.Wrappers;
using OnionArchitectureApp.Domain.Entities;
using System.Net;

namespace OnionArchitectureApp.Application.Features.Commands.Products;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseWrapper<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseWrapper<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = _mapper.Map<Product>(request);
        newProduct.Validate();

        await _unitOfWork.ProductRepository.CreateAsync(newProduct);
        await _unitOfWork.CompleteAsync();

        return  await ResponseWrapper<Guid>.SuccessResult(newProduct.Id, HttpStatusCode.Created);
    }
}
