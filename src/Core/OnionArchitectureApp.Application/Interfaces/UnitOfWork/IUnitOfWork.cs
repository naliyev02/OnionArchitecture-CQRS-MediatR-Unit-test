using Microsoft.EntityFrameworkCore.Storage;
using OnionArchitectureApp.Application.Interfaces.Repositories;

namespace OnionArchitectureApp.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<int> CompleteAsync();
    public IProductRepository ProductRepository { get; }
    public IProductTypeRepository ProductTypeRepository { get; }
    public IProductCategoryRepository ProductCategoryRepository { get; }
    public IProductCategoryRelRepository ProductCategoryRelRepository { get; }
}
