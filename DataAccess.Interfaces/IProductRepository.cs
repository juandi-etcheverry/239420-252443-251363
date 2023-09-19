using Domain;

namespace DataAccess.Interfaces;

public interface IProductRepository
{
    public Product AddProduct(Product product);
    public Product? GetProduct(int id);
    public List<Product> GetProducts();
}