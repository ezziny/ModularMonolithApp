using System;

namespace Catalog.Products.Features.CreateProduct;

public record CreateProductCommand(string Name,
                                List<string>Category,
                                string Description,
                                string Image,
                                decimal Price);

public record CreateProductResult(Guid Id);
public class CreateProductHandler
{

}
