namespace Catalog.Products.Dtos;

public record class ProductDto
(
    Guid Id, 
    List<string> Category,
    string Description,
    string Image,
    string Name,    
    decimal Price
);
