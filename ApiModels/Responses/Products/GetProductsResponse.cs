using Domain;

namespace ApiModels.Responses.Products;

public class GetProductsResponse
{
    public string Message { get; set; }
    public IList<ProductReponseObject> Products { get; set; }
    public IList<Brand> Brands { get; set; }
    public IList<Category> Categories { get; set; }
    public IList<Color> Colors { get; set; }

    public static ProductReponseObject ToResponseObject(Product product)
    {
        return new ProductReponseObject
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Category = product.Category,
            Brand = product.Brand,
            Colors = product.Colors.ToList(),
            Stock = product.Stock,
            PromotionsApply = product.PromotionsApply
        };
    }

    public static IList<Brand> GetBrands(IList<Product> products)
    {
        return products.Select(p => p.Brand).Distinct().ToList();
    }

    public static IList<Category> GetCategories(IList<Product> products)
    {
        return products.Select(p => p.Category).Distinct().ToList();
    }

    public static IList<Color> GetColors(IList<Product> products)
    {
        var colors = new List<Color>();
        foreach (var product in products)
        {
            foreach (var color in product.Colors)
            {
                colors.Add(color);
            }
        }

        return colors.Distinct().ToList();
    }
}

public class ProductReponseObject
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public Category Category { get; set; }
    public Brand Brand { get; set; }
    public IList<Color> Colors { get; set; }
    public int Stock { get; set; }
    public bool PromotionsApply { get; set; }
}