using AutoMapper;
using MediatR;
using OnionArchitectureApp.Application.Exceptions.ProductTypeExceptions;
using OnionArchitectureApp.Application.Interfaces.UnitOfWork;
using OnionArchitectureApp.Application.Wrappers;

namespace OnionArchitectureApp.Application.Features.Commands.ProductTypes;

public class UpdateProductTypeCommandHandler : IRequestHandler<UpdateProductTypeCommand, ResponseWrapper<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseWrapper<Guid>> Handle(UpdateProductTypeCommand request, CancellationToken cancellationToken)
    {
        var productType = await _unitOfWork.ProductTypeRepository.GetByIdAsync(request.Id);
        if (productType is null)
            throw new ProductTypeNotFoundException(request.Id);

        productType.Name = request.Name;
        productType.Description = request.Description;

        await _unitOfWork.CompleteAsync();

        return await ResponseWrapper<Guid>.SuccessResult(productType.Id, System.Net.HttpStatusCode.Accepted);
    }
}
