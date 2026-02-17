using System;
using Catalog.Data;
using Catalog.Products.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SharedKernel.CQRSStuff;

namespace Catalog.Products.Features.GetProduct;
//handler query and resilt 

public record GetProductsResult(IEnumerable<ProductDto> Products);
public record GetProductsQuery() : IQuery<GetProductsResult>;
public class GetProductsHandler : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    private readonly CatalogDBContext _context;

    public GetProductsHandler(CatalogDBContext context)
    {
        _context = context;
    }

    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await _context.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var productDtos = products.Adapt<List<ProductDto>>();
        return new GetProductsResult(productDtos);
    }
}
