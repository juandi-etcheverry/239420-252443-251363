using Domain;

namespace ApiModels.Responses.Products;

public class GetProductsResponse
{
    public string Message { get; set; }
    public IList<ProductReponseObject> Products { get; set; }
    public IList<string> Brands { get; set; }
    public IList<string> Categories { get; set; }

    public static ProductReponseObject ToResponseObject(Product product)
    {
        return new ProductReponseObject
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Category = product.Category.Name,
            Brand = product.Brand.Name,
            Colors = product.Colors.Select(c => c.Name).ToList(),
            Stock = product.Stock,
        };
    }

    public static IList<string> GetBrands(IList<Product> products)
    {
        return products.Select(p => p.Brand.Name).Distinct().ToList();
    }

    public static IList<string> GetCategories(IList<Product> products)
    {
        return products.Select(p => p.Category.Name).Distinct().ToList();
    }
}

public class ProductReponseObject
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public string Category { get; set; }
    public string Brand { get; set; }
    public IList<string> Colors { get; set; }
    public int Stock { get; set; }
}