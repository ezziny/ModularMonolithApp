using System;
using System.Reflection;
using Catalog.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Data;

public class CatalogDBContext : DbContext
{
    public CatalogDBContext(DbContextOptions<CatalogDBContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("catalog");
        builder.Entity<Product>().HasKey(k => k.Id);
        builder.Entity<Product>().Property(k => k.Name).HasMaxLength(100).IsRequired();
        builder.Entity<Product>().Property(k => k.Category).IsRequired();
        builder.Entity<Product>().Property(k => k.Description).HasMaxLength(500);
        builder.Entity<Product>().Property(k => k.Price).IsRequired();
        base.OnModelCreating(builder);
    }
}
