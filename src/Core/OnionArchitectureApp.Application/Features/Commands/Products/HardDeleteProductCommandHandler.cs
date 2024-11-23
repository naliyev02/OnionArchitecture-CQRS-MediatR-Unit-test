using AutoMapper;
using MediatR;
using OnionArchitectureApp.Application.Exceptions.ProductExceptions;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Wrappers;

namespace OnionArchitectureApp.Application.Features.Commands.Products;

public class HardDeleteProductCommandHandler : IRequestHandler<HardDeleteProductCommand, ResponseWrapper<Guid>>
{
    private readonly IProductRepository _repository;

    public HardDeleteProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }
    public async Task<ResponseWrapper<Guid>> Handle(HardDeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id);
        if (product is null)
            throw new ProductNotFoundException(request.Id);

        _repository.Delete(product);
        await _repository.SaveAsync();

        return await ResponseWrapper<Guid>.SuccessResult(product.Id, System.Net.HttpStatusCode.Accepted);
    }
}
