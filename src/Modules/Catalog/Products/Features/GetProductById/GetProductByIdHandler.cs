using System;
using Catalog.Data;
using Catalog.Products.Dtos;
using Catalog.Products.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SharedKernel.CQRSStuff;

namespace Catalog.Products.Features.GetProductById;

public record GetProductByIdResult(ProductDto Product);
public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public class GetProductByIdHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    private readonly CatalogDBContext _context;

    public GetProductByIdHandler(CatalogDBContext context)
    {
        _context = context;
    }

    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken) ?? throw new ProductNotFoundException(query.Id);
        var productDto = product.Adapt<ProductDto>();


        return new GetProductByIdResult(productDto);
    }
}
