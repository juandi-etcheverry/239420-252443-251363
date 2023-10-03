using Domain;

namespace Logic.Interfaces;

public interface IProductLogic
{
    Product GetProduct(int id);
    List<Product> GetProducts();
    List<Product> GetProducts(Func<Product, bool> predicate);
    Product AddProduct(Product product);
}