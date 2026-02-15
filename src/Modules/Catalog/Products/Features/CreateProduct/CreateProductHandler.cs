using System;
using Catalog.Data;
using Catalog.Products.Dtos;
using Catalog.Products.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal;
using SharedKernel.CQRSStuff;

namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(ProductDto ProductDto) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    private readonly CatalogDBContext _context;

    public CreateProductHandler(CatalogDBContext context)
    {
        _context = context;
    }
    Product CreateProduct(ProductDto productDto)
    {
        var product = Product.Create(
            Guid.NewGuid(),
            productDto.Name ,
            productDto.Category ,
            productDto.Description,
            productDto.Image ,
            productDto.Price 
        );
        return product;
    }
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = CreateProduct(command.ProductDto);
        await _context.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);
    }
}
