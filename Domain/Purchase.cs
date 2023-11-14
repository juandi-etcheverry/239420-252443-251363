namespace Domain;

public class Purchase
{
    public int Id { get; set; }
    public List<Product> Products { get; set; } = new();

    public User User { get; set; }

    public float TotalPrice { get; set; }
    public float FinalPrice { get; set; }

    public string? PromotionName
    {
        get;
        set;
    }
    public string? PaymentMethod { get; set; }

    public void AddProducts(List<Product> products)
    {
        foreach (var p in products) AddProduct(p);
        CalculateTotalPrice();
    }

    public Product AddProduct(Product product)
    {
        if (product == null) throw new ArgumentException("Product is null");
        if (!Products.Contains(product)) Products.Add(product);
        return product;
    }

    public Product DeleteProduct(Product product)
    {
        if (product == null) throw new ArgumentException("Product is null");
        Products.Remove(product);
        return product;
    }

    public void AssignUser(User user)
    {
        User = user;
    }

    private void CalculateTotalPrice()
    {
        TotalPrice = Products.Sum(p => p.Price);
    }
}

