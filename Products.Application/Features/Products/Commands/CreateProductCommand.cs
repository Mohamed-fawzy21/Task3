using MediatR;

namespace Products.Application.Features.Products.Commands;

public sealed record CreateProductCommand(string Name, decimal Price) : IRequest<int>;
