using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Domain.Entities;
using OnionArchitectureApp.Persistence.Context;

namespace OnionArchitectureApp.Persistence.Repositories;

public class ProductCategoryRepository : GenericRepository<ProductCategory>, IProductCategoryRepository
{
    private readonly AppDbContext context;

    public ProductCategoryRepository(AppDbContext context) : base(context)
    {
        this.context = context;
    }
}
