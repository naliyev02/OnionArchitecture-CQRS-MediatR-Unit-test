using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Domain.Entities;
using OnionArchitectureApp.Persistence.Context;

namespace OnionArchitectureApp.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly AppDbContext context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        this.context = context;
    }
}
