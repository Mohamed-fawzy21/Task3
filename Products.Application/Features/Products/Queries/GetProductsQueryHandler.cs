using MediatR;
using Products.Application.Abstractions.Persistence;
using Products.Application.DTOs;

namespace Products.Application.Features.Products.Queries;

public sealed class GetProductsQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync(cancellationToken);
        return products.Select(product => new ProductDto(product.Id, product.Name, product.Price)).ToList();
    }
}
