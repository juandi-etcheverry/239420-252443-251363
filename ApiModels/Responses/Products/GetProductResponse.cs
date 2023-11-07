using Domain;

namespace ApiModels.Responses.Products;

public class GetProductResponse
{
    public Guid id { get; set; }
    public string Message { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public string Description { get; set; }
    public Brand Brand { get; set; }
    public Category Category { get; set; }
    public IList<Color> Colors { get; set; }
    public int Stock { get; set; }
}