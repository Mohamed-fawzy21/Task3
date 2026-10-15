namespace Products.Domain.Entities;

public sealed class Product
{
    private Product()
    {
    }

    public Product(string name, decimal price)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Product name cannot be empty.", nameof(name));
        }

        var normalizedName = name.Trim();
        if (normalizedName.Length > 200)
        {
            throw new ArgumentException("Product name cannot exceed 200 characters.", nameof(name));
        }

        if (price <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(price), "Product price must be greater than zero.");
        }

        Id = 0;
        Name = normalizedName;
        Price = price;
    }

    public int Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public decimal Price { get; private set; }
}
