using AutoMapper;
using MediatR;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Wrappers;
using OnionArchitectureApp.Domain.Entities;
using System.Net;

namespace OnionArchitectureApp.Application.Features.Commands.Products;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseWrapper<Guid>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseWrapper<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = _mapper.Map<Product>(request);

        await _repository.CreateAsync(newProduct);
        await _repository.SaveAsync();

        return  await ResponseWrapper<Guid>.SuccessResult(newProduct.Id, HttpStatusCode.Created);
    }
}
