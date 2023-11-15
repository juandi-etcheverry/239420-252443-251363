namespace Domain;

public class Purchase
{
    public int Id { get; set; }
    public List<PurchaseProduct> Products { get; set; } = new();

    public User User { get; set; }

    public float TotalPrice { get; set; }
    public float FinalPrice { get; set; }

    public string? PromotionName
    {
        get;
        set;
    }
    public string? PaymentMethod { get; set; }

    public void AddProducts(List<PurchaseProduct> products)
    {
        foreach (var p in products) AddProduct(p);
        CalculateTotalPrice();
    }

    public PurchaseProduct AddProduct(PurchaseProduct product)
    {
        if (product == null || product.Product == null) throw new ArgumentException("Product is null");
        if (!Products.Contains(product)) Products.Add(product);
        return product;
    }

    public PurchaseProduct DeleteProduct(PurchaseProduct product)
    {
        if (product == null || product.Product == null) throw new ArgumentException("Product is null");
        Products.Remove(product);
        return product;
    }

    public void AssignUser(User user)
    {
        User = user;
    }

    private void CalculateTotalPrice()
    {
        TotalPrice = Products.Sum(p => p.Product.Price * p.Quantity);
    }
}

public class PurchaseProduct
{
    public int Id { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}

