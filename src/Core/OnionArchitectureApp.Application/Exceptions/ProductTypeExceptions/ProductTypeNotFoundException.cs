using OnionArchitectureApp.Application.Exceptions.Common;

namespace OnionArchitectureApp.Application.Exceptions.ProductTypeExceptions;

public class ProductTypeNotFoundException : BaseException
{
    public Guid ProductTypeId { get; set; }
    public ProductTypeNotFoundException(Guid productTypeId) : base($"Product type with {productTypeId} ID was not found.", 404)
    {
        ProductTypeId = productTypeId;
    }
}
