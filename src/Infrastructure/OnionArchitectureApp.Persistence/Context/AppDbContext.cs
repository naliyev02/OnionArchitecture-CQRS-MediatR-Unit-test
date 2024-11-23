using Microsoft.EntityFrameworkCore;
using OnionArchitectureApp.Domain.Common;
using OnionArchitectureApp.Domain.Entities;
using OnionArchitectureApp.Persistence.Configurations;

namespace OnionArchitectureApp.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<ProductCategoryRel> ProductCategoryRels { get; set; }
    public DbSet<ProductCategory> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
                default:
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
