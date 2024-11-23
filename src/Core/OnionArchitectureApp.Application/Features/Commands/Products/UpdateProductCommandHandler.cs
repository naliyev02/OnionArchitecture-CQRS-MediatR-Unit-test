using AutoMapper;
using MediatR;
using OnionArchitectureApp.Application.Exceptions.ProductExceptions;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Wrappers;
using OnionArchitectureApp.Domain.Entities;
using System.Net;

namespace OnionArchitectureApp.Application.Features.Commands.Products;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseWrapper<Guid>>
{
    private readonly IProductRepository _repository;
    private readonly IProductCategoryRelRepository _categoryRelRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository repository, IProductCategoryRelRepository categoryRelRepository, IMapper mapper )
    {
        _repository = repository;
        _categoryRelRepository = categoryRelRepository;
        _mapper = mapper;
    }

    public async Task<ResponseWrapper<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id);
        if (product is null)
            throw new ProductNotFoundException(request.Id);

        var transaction = await _repository.BeginTransactionAsync();
        try
        {
            product.Name = request.Name ?? product.Name;
            product.Description = request.Description ?? product.Description;
            product.BrandName = request.BrandName ?? product.BrandName;
            product.Price = request.Price ?? product.Price;
            product.StockQuantity = request.StockQuantity ?? product.StockQuantity;
            product.IsActive = request.IsActive ?? product.IsActive;
            product.TypeId = request.TypeId ?? product.TypeId;


            if (request.ProductCategoryRels is not null)
            {
                var RelToRemove = product.ProductCategoryRels?.Where(r => !request.ProductCategoryRels.Any(dto => dto.Id == r.Id));
                if (RelToRemove != null)
                    _categoryRelRepository.DeleteRange(RelToRemove);

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

                        await _categoryRelRepository.CreateAsync(newproductCategoryRel);
                        await _repository.SaveAsync();
                    }
                }
            }

            await _repository.SaveAsync();
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
