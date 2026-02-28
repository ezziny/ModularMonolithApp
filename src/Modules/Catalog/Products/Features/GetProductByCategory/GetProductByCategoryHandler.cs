using System;
using Catalog.Data;
using Catalog.Products.Dtos;
using Mapster;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SharedKernel.CQRSStuff;
using SharedKernel.Pagination;

namespace Catalog.Products.Features.GetProductByCategory;

public record GetProductByCategoryResult(PaginatedResult<ProductDto> Products);
public record GetProductByCategoryQuery(string Category, PaginatedRequest PaginatedRequest) : IQuery<GetProductByCategoryResult>;

public class GetProductByCategoryHandler : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    private readonly CatalogDBContext _context;

    public GetProductByCategoryHandler(CatalogDBContext context)
    {
        _context = context;
    }

    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginatedRequest.PageIndex;
        var pageSize = query.PaginatedRequest.PageSize;

        var filtered = _context.Products.Where(p => p.Category.Contains(query.Category));
        var totalCount = await filtered.LongCountAsync(cancellationToken);
        var productDtos = await filtered
            .AsNoTracking()
            .Where(p => p.Category.Contains(query.Category))
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ProjectToType<ProductDto>()
            .ToListAsync(cancellationToken);

        var paginatedResult = new PaginatedResult<ProductDto>(pageIndex, pageSize, totalCount, productDtos);

        return new GetProductByCategoryResult(paginatedResult);
    }
}
