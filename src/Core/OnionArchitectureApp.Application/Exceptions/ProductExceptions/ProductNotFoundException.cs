using OnionArchitectureApp.Application.Exceptions.Common;

namespace OnionArchitectureApp.Application.Exceptions.ProductExceptions;

public class ProductNotFoundException : BaseException
{
    public Guid ProductId { get; set; }
    public ProductNotFoundException(Guid productId) : base($"Product with {productId} ID was not found.", 404)
    {
        ProductId = productId;
    }
}
