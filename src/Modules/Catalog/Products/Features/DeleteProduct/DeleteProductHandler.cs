using Catalog.Data;
using Catalog.Products.Dtos;
using Catalog.Products.Models;
using MediatR;
using SharedKernel.CQRSStuff;

namespace Catalog.Products.Features.DeleteProduct;


public record DeleteProductCommand(ProductDto ProductDto) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccessful);

public class DeleteProductHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    private readonly CatalogDBContext _context;

    public DeleteProductHandler(CatalogDBContext context)
    {
        _context = context;
    }
    private async Task DeleteProduct(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(productId, cancellationToken) ?? throw new Exception($"Product with id {productId} not found");
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        await DeleteProduct(command.ProductDto.Id, cancellationToken);
        return new DeleteProductResult(true); //TODO maybe return unit or add an exception later bc i don't like how we're always returning true
    }
}
