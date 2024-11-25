using Microsoft.EntityFrameworkCore.Storage;
using OnionArchitectureApp.Application.Interfaces.Repositories;
using OnionArchitectureApp.Application.Interfaces.UnitOfWork;
using OnionArchitectureApp.Persistence.Context;

namespace OnionArchitectureApp.Persistence.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IProductRepository ProductRepository { get; }
    public IProductTypeRepository ProductTypeRepository { get; }
    public IProductCategoryRepository ProductCategoryRepository { get; }
    public IProductCategoryRelRepository ProductCategoryRelRepository { get; }

    public UnitOfWork(AppDbContext context, IProductRepository productRepository, IProductTypeRepository productTypeRepository, IProductCategoryRepository productCategoryRepository, IProductCategoryRelRepository productCategoryRelRepository)
    {
        _context = context;
        ProductRepository = productRepository;
        ProductTypeRepository = productTypeRepository;
        ProductCategoryRepository = productCategoryRepository;
        ProductCategoryRelRepository = productCategoryRelRepository;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
    
    public async ValueTask DisposeAsync() { }
}
