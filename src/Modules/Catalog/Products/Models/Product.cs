using Catalog.Products.Events;
using SharedKernel.DDD;

namespace Catalog.Products.Models;

public class Product : Aggregate<Guid>
{
    //she private on my set until it i get.. anyways it's private set for encapsulation
    public string Name { get; private set; } = default!;
    public List<string> Category { get; private set; } = [];
    public string Description { get; private set; } = default!;
    public string Image { get; private set; } = default!;
    public decimal Price { get; private set; }
    public static Product Create(Guid id, string name,
                                List<string> category,
                                string description,
                                string image,
                                decimal Price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(Price);
        var product = new Product
        {
            Category = category,
            Id = id,
            Description = description,
            Image = image,
            Name = name,
            Price = Price
        };
        product.AddDomainEvent(new ProductCreatedEvent(product));
        return product;
    }
    public void Update(string name, List<string> category, string description, string image, decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        
        Name = name;
        Category = category;
        Description = description;
        Image = image;
        if (Price != price)
        {
            Price = price;
            AddDomainEvent(new ProductPriceChangedEvent(this));
        }
    }
}
