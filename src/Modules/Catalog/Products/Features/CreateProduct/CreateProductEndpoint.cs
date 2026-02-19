using System;
using Carter;
using Catalog.Products.Dtos;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Catalog.Products.Features.CreateProduct;

public record CreateProductRequest(ProductDto ProductDto);
public record CreateProductResponse(Guid Id);
public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products" , async(CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"/products/{response.Id}", response);
        }
        ) //then we do the boring openAPI/swagger stuff 
        .WithName("CreateProduct")
        .WithTags("Products")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesValidationProblem()
        .WithSummary("Create a new product")
        .WithDescription("Creates a new product in the catalog")
        ;
    }
}
