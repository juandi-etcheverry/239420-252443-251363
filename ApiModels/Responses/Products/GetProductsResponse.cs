using Domain;

namespace ApiModels.Responses.Products;

public class GetProductsResponse
{
    public string Message { get; set; }
    public IList<ProductReponseObject> Products { get; set; }

    public static ProductReponseObject ToResponseObject(Product product)
    {
        return new ProductReponseObject
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Category = product.Category,
            Brand = product.Brand,
            Colors = product.Colors
        };
    }
}

public class ProductReponseObject
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public Category Category { get; set; }
    public Brand Brand { get; set; }
    public IList<Color> Colors { get; set; }
}