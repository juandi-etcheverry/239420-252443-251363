using Domain;

namespace DataAccess.Interfaces;

public interface IProductRepository
{
    public Product AddProduct(Product product);
    public Product GetProduct(Guid id);
    public List<Product> GetProducts();
    public List<Product> GetProducts(Func<Product, bool> predicate);
    public Product SoftDelete(Guid id);
    public Product UpdateProduct(Guid id, Product product);
}