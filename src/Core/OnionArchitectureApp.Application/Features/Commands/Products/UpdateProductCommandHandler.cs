using AutoMapper;
using MediatR;
using OnionArchitectureApp.Application.Exceptions.ProductExceptions;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Interfaces.UnitOfWork;
using OnionArchitectureApp.Application.Wrappers;
using OnionArchitectureApp.Domain.Entities;
using System.Net;

namespace OnionArchitectureApp.Application.Features.Commands.Products;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseWrapper<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseWrapper<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
        if (product is null)
            throw new ProductNotFoundException(request.Id);

        var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            product.Name = request.Name;
            product.Description = request.Description;
            product.BrandName = request.BrandName;
            product.TypeId = request.TypeId;

            product.UpdatePrice(request.Price);
            product.UpdateStockQuantity(request.StockQuantity);

            if (request.ProductCategoryRels is not null)
            {
                var RelToRemove = product.ProductCategoryRels?.Where(r => !request.ProductCategoryRels.Any(dto => dto.Id == r.Id));
                if (RelToRemove != null)
                    _unitOfWork.ProductCategoryRelRepository.DeleteRange(RelToRemove);

                foreach (var productCategoryRel in request.ProductCategoryRels)
                {
                    var existingRel = product.ProductCategoryRels?.FirstOrDefault(r => r.Id == productCategoryRel.Id);

                    if (existingRel != null)
                    {
                        existingRel.CategoryId = productCategoryRel.CategoryId!.Value;
                    }
                    else
                    {
                        var newproductCategoryRel = new ProductCategoryRel
                        {
                            CategoryId = productCategoryRel.CategoryId!.Value
                        };

                        await _unitOfWork.ProductCategoryRelRepository.CreateAsync(newproductCategoryRel);
                        await _unitOfWork.CompleteAsync();
                    }
                }
            }

            await _unitOfWork.CompleteAsync();
            await transaction.CommitAsync();
            await transaction.DisposeAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return await ResponseWrapper<Guid>.ErrorResult(ex.Message, HttpStatusCode.Conflict);
        }


        return await ResponseWrapper<Guid>.SuccessResult(product.Id, HttpStatusCode.OK);
    }
}
