using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Domain.Entities;
using OnionArchitectureApp.Persistence.Context;

namespace OnionArchitectureApp.Persistence.Repositories;

public class ProductCategoryRelRepository : GenericRepository<ProductCategoryRel>, IProductCategoryRelRepository
{
    private readonly AppDbContext context;

    public ProductCategoryRelRepository(AppDbContext context) : base(context)
    {
        this.context = context;
    }
}
