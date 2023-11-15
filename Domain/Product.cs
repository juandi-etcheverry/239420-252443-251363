namespace Domain;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public Category Category { get; set; } = null;
    public Brand Brand { get; set; } = null;
    public List<Color> Colors { get; set; }
    public bool IsDeleted { get; set; } = false;
    public int Stock { get; set; }
    public bool PromotionsApply { get; set; } = true;
}

public class PurchaseProductRequest
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}