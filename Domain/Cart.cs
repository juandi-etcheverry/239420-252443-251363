namespace Domain;

public class Cart
{
    public int Id { get; private set; }
    public List<Product> Products { get; set; } = new List<Product>();
    public SessionToken Session { get; set; }
    
    public Product AddProduct(Product product)
    {
       if(product == null) throw new ArgumentException("Product is null");
        Products.Add(product);
        return product;
    }
}