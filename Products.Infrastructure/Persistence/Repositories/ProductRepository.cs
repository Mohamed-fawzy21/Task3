using Microsoft.EntityFrameworkCore;
using Products.Application.Abstractions.Persistence;
using Products.Domain.Entities;

namespace Products.Infrastructure.Persistence.Repositories;

public sealed class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken) =>
        dbContext.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(product => product.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken) =>
        await dbContext.Products
            .AsNoTracking()
            .OrderBy(product => product.Id)
            .ToListAsync(cancellationToken);

    public async Task AddAsync(Product product, CancellationToken cancellationToken) =>
        await dbContext.Products.AddAsync(product, cancellationToken);
}
