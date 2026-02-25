using System;
using Catalog.Data;
using Catalog.Products.Dtos;
using Catalog.Products.Exceptions;
using Catalog.Products.Models;
using FluentValidation;

using SharedKernel.CQRSStuff;

namespace Catalog.Products.Features.UpdateProduct;

//you need a handler, a command and a result
public record UpdateProductCommand(ProductDto ProductDto): ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSuccessful);
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.ProductDto.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(p => p.ProductDto.Name).NotEmpty().WithMessage("name is required");
        RuleFor(p => p.ProductDto.Price).GreaterThan(0).WithMessage("Price must be over 0");    
    }
}
public class UpdateProductHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly CatalogDBContext _context;

    public UpdateProductHandler(CatalogDBContext context)
    {
        _context = context;
    }
    private async Task<Product> UpdateProduct(ProductDto productDto, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(productDto.Id, cancellationToken) ?? throw new ProductNotFoundException(productDto.Id);
        product.Update(
            productDto.Name,
            productDto.Category,
            productDto.Description,
            productDto.Image,
            productDto.Price
        );
        // _context.Update(product); //i guess you don't need it bc of the change tracker
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        await UpdateProduct(command.ProductDto, cancellationToken);
        return new UpdateProductResult(true); //TODO maybe return unit or add an exception later bc i don't like how we're always returning true
    }
}
