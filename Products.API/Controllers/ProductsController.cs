using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.DTOs;
using Products.Application.Features.Products.Commands;
using Products.Application.Features.Products.Queries;

namespace Products.API.Controllers;

[ApiController]
[Route("api/products")]
[Produces("application/json")]
public sealed class ProductsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ProductDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProductDto>>> GetAll(CancellationToken cancellationToken)
    {
        var products = await sender.Send(new GetProductsQuery(), cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> GetById(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "Product not found",
                detail: $"No product with ID {id} exists.");
        }

        var product = await sender.Send(new GetProductByIdQuery(id), cancellationToken);
        return product is null
            ? Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "Product not found",
                detail: $"No product with ID {id} exists.")
            : Ok(product);
    }

    [HttpPost]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> Create(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var id = await sender.Send(new CreateProductCommand(request.Name, request.Price), cancellationToken);
        var createdProduct = new ProductDto(id, request.Name.Trim(), request.Price);

        return CreatedAtAction(nameof(GetById), new { id }, createdProduct);
    }
}
