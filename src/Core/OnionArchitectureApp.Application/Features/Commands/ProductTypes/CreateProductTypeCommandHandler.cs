using AutoMapper;
using MediatR;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Wrappers;
using OnionArchitectureApp.Domain.Entities;

namespace OnionArchitectureApp.Application.Features.Commands.ProductTypes;

public class CreateProductTypeCommandHandler : IRequestHandler<CreateProductTypeCommand, ResponseWrapper<Guid>>
{
    private readonly IProductTypeRepository _repository;
    private readonly IMapper _mapper;

    public CreateProductTypeCommandHandler(IProductTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseWrapper<Guid>> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<ProductType>(request);
        await _repository.CreateAsync(product);

        await _repository.SaveAsync();

        return await ResponseWrapper<Guid>.SuccessResult(product.Id, System.Net.HttpStatusCode.Created);
    }
}
