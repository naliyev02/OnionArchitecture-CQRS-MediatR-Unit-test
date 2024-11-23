using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using OnionArchitectureApp.Application.Exceptions.ProductExceptions;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Wrappers;

namespace OnionArchitectureApp.Application.Features.Commands.Products;

public class SoftDeleteProductCommandHandler : IRequestHandler<SoftDeleteProductCommand, ResponseWrapper<Guid>>
{
    private readonly IProductRepository _repository;

    public SoftDeleteProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseWrapper<Guid>> Handle(SoftDeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id);
        if (product is null)
            throw new ProductNotFoundException(request.Id);

        product.IsDeleted = true;
        await _repository.SaveAsync();

        return await ResponseWrapper<Guid>.SuccessResult(product.Id, System.Net.HttpStatusCode.Accepted);
    }
}
