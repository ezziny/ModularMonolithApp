using System;
using Catalog.Data;
using Catalog.Products.Dtos;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SharedKernel.CQRSStuff;
using SharedKernel.Pagination;

namespace Catalog.Products.Features.GetProduct;
//handler query and resilt 

public record GetProductsResult(PaginatedResult<ProductDto> Products);
public record GetProductsQuery(PaginatedRequest PaginatedRequest) : IQuery<GetProductsResult>;
public class GetProductsHandler : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    private readonly CatalogDBContext _context;

    public GetProductsHandler(CatalogDBContext context)
    {
        _context = context;
    }

    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginatedRequest.PageIndex;
        var pageSize = query.PaginatedRequest.PageSize;
        
        var totalCount = await _context.Products.LongCountAsync(cancellationToken);
        
        var productDtos = await _context.Products
            .AsNoTracking()
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ProjectToType<ProductDto>()
            .ToListAsync(cancellationToken);

        var paginatedResult = new PaginatedResult<ProductDto>(pageIndex, pageSize, totalCount, productDtos);
        
        return new GetProductsResult(paginatedResult);
    }
}
