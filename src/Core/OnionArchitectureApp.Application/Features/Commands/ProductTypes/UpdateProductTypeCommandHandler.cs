using AutoMapper;
using MediatR;
using OnionArchitectureApp.Application.Exceptions.ProductTypeExceptions;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Wrappers;

namespace OnionArchitectureApp.Application.Features.Commands.ProductTypes;

public class UpdateProductTypeCommandHandler : IRequestHandler<UpdateProductTypeCommand, ResponseWrapper<Guid>>
{
    private readonly IProductTypeRepository _repository;
    private readonly IMapper _mapper;

    public UpdateProductTypeCommandHandler(IProductTypeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseWrapper<Guid>> Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
    {
        var productType = await _repository.GetByIdAsync(request.Id);
        if (productType is null)
            throw new ProductTypeNotFoundException(request.Id);

        productType.Name = request.Name ?? productType.Name;
        productType.Description = request.Description ?? productType.Description;

        await _repository.SaveAsync();

        return await ResponseWrapper<Guid>.SuccessResult(productType.Id, System.Net.HttpStatusCode.Accepted);
    }
}
