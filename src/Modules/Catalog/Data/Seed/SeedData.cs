using Catalog.Products.Models;

namespace Catalog.Data.Seed;

public static class SeedData
{
    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create(
            Guid.Parse("5334c996-8457-4cf0-815c-ed2b77c4ff61"),
            "iPhone 14 Pro",
            new List<string> { "Electronics", "Smartphones" },
            "Latest iPhone with A16 Bionic chip and Dynamic Island",
            "https://images.unsplash.com/photo-1678685888221-cda773a3dcdb",
            999.99m
        ),
        Product.Create(
            Guid.Parse("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
            "Samsung Galaxy S23",
            new List<string> { "Electronics", "Smartphones" },
            "Flagship Samsung phone with advanced camera system",
            "https://images.unsplash.com/photo-1610945415295-d9bbf067e59c",
            899.99m
        ),
        Product.Create(
            Guid.Parse("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8"),
            "MacBook Pro 16\"",
            new List<string> { "Electronics", "Laptops" },
            "Powerful laptop with M2 Pro chip and stunning Liquid Retina XDR display",
            "https://images.unsplash.com/photo-1517336714731-489689fd1ca8",
            2499.99m
        ),
        Product.Create(
            Guid.Parse("6ec1297b-ec0a-4aa1-be25-6726e3b51a27"),
            "Sony WH-1000XM5",
            new List<string> { "Electronics", "Audio", "Headphones" },
            "Industry-leading noise canceling headphones with exceptional sound quality",
            "https://images.unsplash.com/photo-1546435770-a3e426bf472b",
            399.99m
        ),
        Product.Create(
            Guid.Parse("b786103d-c621-4f5a-b498-23452610f88c"),
            "Dell UltraSharp 27\"",
            new List<string> { "Electronics", "Monitors" },
            "4K UHD monitor with USB-C connectivity and adjustable stand",
            "https://images.unsplash.com/photo-1527443224154-c4a3942d3acf",
            549.99m
        ),
        Product.Create(
            Guid.Parse("c4bbc4a2-4555-45d8-97cc-2a99b2167331"),
            "Logitech MX Master 3S",
            new List<string> { "Electronics", "Accessories", "Mouse" },
            "Wireless performance mouse with ultra-fast scrolling and precise tracking",
            "https://images.unsplash.com/photo-1527814050087-3793815479db",
            99.99m
        ),
        Product.Create(
            Guid.Parse("93170c85-7795-489c-8e8f-7dcf3b4f4188"),
            "Mechanical Keyboard RGB",
            new List<string> { "Electronics", "Accessories", "Keyboards" },
            "Premium mechanical keyboard with customizable RGB lighting and Cherry MX switches",
            "https://images.unsplash.com/photo-1595225476474-87563907a212",
            149.99m
        ),
        Product.Create(
            Guid.Parse("3dc0e483-b0cd-4162-8c26-291e0146d9b8"),
            "AirPods Pro 2",
            new List<string> { "Electronics", "Audio", "Earbuds" },
            "Active noise cancellation and Adaptive Audio for personalized listening",
            "https://images.unsplash.com/photo-1606841837239-c5a1a4a07af7",
            249.99m
        ),
        Product.Create(
            Guid.Parse("baaa3cf2-e0c2-4071-a80a-57296b85e5d8"),
            "LG UltraGear Gaming Monitor",
            new List<string> { "Electronics", "Monitors", "Gaming" },
            "27\" 1440p gaming monitor with 165Hz refresh rate and 1ms response time",
            "https://images.unsplash.com/photo-1593640408182-31c70c8268f5",
            449.99m
        ),
        Product.Create(
            Guid.Parse("2c94d72f-5db2-4c4e-8c0b-7c3a5e6c1d7e"),
            "iPad Pro 12.9\"",
            new List<string> { "Electronics", "Tablets" },
            "Most powerful iPad with M2 chip and brilliant Liquid Retina XDR display",
            "https://images.unsplash.com/photo-1544244015-0df4b3ffc6b0",
            1099.99m
        )
    };
}
