using Products.Domain.Entities;

namespace Products.Application.Abstractions.Persistence;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken);

    Task AddAsync(Product product, CancellationToken cancellationToken);
}
