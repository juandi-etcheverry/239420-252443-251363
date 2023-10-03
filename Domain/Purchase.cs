namespace Domain;

public class Purchase
{
    public int Id { get; private set; }
    public List<Product> Products { get; set; } = new List<Product>();
    
    public User User { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public void AddProducts(List<Product> products)
    {
        foreach (var p in products) AddProduct(p);
    }
    
    public Product AddProduct(Product product)
    {
       if(product == null) throw new ArgumentException("Product is null");
       if (!Products.Contains(product))
       {
           Products.Add(product);
       }
       return product;
    }
    public Product DeleteProduct(Product product)
    {
        if(product == null) throw new ArgumentException("Product is null");
        Products.Remove(product);
        return product;
    }
    public void AssignUser(User user)
    {
        User = user;
    }
    


}