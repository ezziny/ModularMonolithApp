using System;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Data;

namespace Catalog.Data.Seed;

public class CatalogDataSeeder : IDataSeeder
{
    private readonly CatalogDBContext _context;

    public CatalogDataSeeder(CatalogDBContext context)
    {
        _context = context;
    }
    public async Task SeedAsync()
    {
        if (!await _context.Products.AnyAsync())
        {
            await _context.Products.AddRangeAsync(SeedData.Products);
            await _context.SaveChangesAsync();
        }
    }
}
