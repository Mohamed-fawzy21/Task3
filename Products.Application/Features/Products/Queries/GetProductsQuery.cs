using MediatR;
using Products.Application.DTOs;

namespace Products.Application.Features.Products.Queries;

public sealed record GetProductsQuery : IRequest<List<ProductDto>>;
