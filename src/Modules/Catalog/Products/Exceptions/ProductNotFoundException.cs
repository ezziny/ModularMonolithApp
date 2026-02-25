using System;
using SharedKernel.Exceptions;

namespace Catalog.Products.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid Id) : base("Product", Id)
    {
        
    }
}
