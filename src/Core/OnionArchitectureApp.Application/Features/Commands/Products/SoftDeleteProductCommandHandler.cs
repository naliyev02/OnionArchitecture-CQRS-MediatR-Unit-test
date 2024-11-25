using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using OnionArchitectureApp.Application.Exceptions.ProductExceptions;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Interfaces.UnitOfWork;
using OnionArchitectureApp.Application.Wrappers;

namespace OnionArchitectureApp.Application.Features.Commands.Products;

public class SoftDeleteProductCommandHandler : IRequestHandler<SoftDeleteProductCommand, ResponseWrapper<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public SoftDeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseWrapper<Guid>> Handle(SoftDeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
        if (product is null)
            throw new ProductNotFoundException(request.Id);

        product.IsDeleted = true;
        await _unitOfWork.CompleteAsync();

        return await ResponseWrapper<Guid>.SuccessResult(product.Id, System.Net.HttpStatusCode.Accepted);
    }
}
