using AutoMapper;
using MediatR;
using OnionArchitectureApp.Application.Exceptions.ProductExceptions;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Interfaces.UnitOfWork;
using OnionArchitectureApp.Application.Wrappers;

namespace OnionArchitectureApp.Application.Features.Commands.Products;

public class HardDeleteProductCommandHandler : IRequestHandler<HardDeleteProductCommand, ResponseWrapper<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;

    public HardDeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseWrapper<Guid>> Handle(HardDeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
        if (product is null)
            throw new ProductNotFoundException(request.Id);

        _unitOfWork.ProductRepository.Delete(product);
        await _unitOfWork.CompleteAsync();

        return await ResponseWrapper<Guid>.SuccessResult(product.Id, System.Net.HttpStatusCode.Accepted);
    }
}
