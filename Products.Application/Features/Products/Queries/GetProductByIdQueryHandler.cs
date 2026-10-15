using MediatR;
using Products.Application.Abstractions.Persistence;
using Products.Application.DTOs;

namespace Products.Application.Features.Products.Queries;

public sealed class GetProductByIdQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        return product is null ? null : new ProductDto(product.Id, product.Name, product.Price);
    }
}
