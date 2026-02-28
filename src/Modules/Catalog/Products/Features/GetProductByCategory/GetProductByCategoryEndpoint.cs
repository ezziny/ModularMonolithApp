using System;
using Carter;
using Catalog.Products.Dtos;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharedKernel.Pagination;

namespace Catalog.Products.Features.GetProductByCategory;

public record GetProductByCategoryResponse(PaginatedResult<ProductDto> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async ([AsParameters] PaginatedRequest request, string category, ISender sender) =>
        {
            var query = new GetProductByCategoryQuery(category, request);
            var result = await sender.Send(query);
            var response = result.Adapt<GetProductByCategoryResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProductByCategory")
        .WithTags("Products")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .WithSummary("Get products by category")
        .WithDescription("Retrieves all products that belong to a specific category");
    }
}
