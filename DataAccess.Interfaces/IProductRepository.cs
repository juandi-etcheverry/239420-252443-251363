using Domain;

namespace DataAccess.Interfaces;

public interface IProductRepository
{
    public Product AddProduct(Product product);
    public Product GetProduct(int id);
    public List<Product> GetProducts();
    public List<Product> GetProducts(Func<Product, bool> predicate);
    public Product SoftDelete(int id);
}