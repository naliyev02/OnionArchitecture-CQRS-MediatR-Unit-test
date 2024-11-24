using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Domain.Entities;
using OnionArchitectureApp.Persistence.Context;

namespace OnionArchitectureApp.Persistence.Repositories;

public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
{
    private readonly AppDbContext context;

    public ProductTypeRepository(AppDbContext context) : base(context)
    {
        this.context = context;
    }
}
