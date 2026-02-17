using System;
using Catalog.Data;
using Catalog.Products.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SharedKernel.CQRSStuff;

namespace Catalog.Products.Features.GetProductByCategory;

public record GetProductByCategoryResult(IEnumerable<ProductDto> Products);
public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public class GetProductByCategoryHandler : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    private readonly CatalogDBContext _context;

    public GetProductByCategoryHandler(CatalogDBContext context)
    {
        _context = context;
    }

    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .AsNoTracking()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);

        var productDtos = products.Adapt<List<ProductDto>>();


        return new GetProductByCategoryResult(productDtos);
    }
}
