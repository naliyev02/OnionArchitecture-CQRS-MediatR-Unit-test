using Microsoft.EntityFrameworkCore;
using OnionArchitectureApp.Domain.Entities;

namespace OnionArchitectureApp.Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

}
