using Domain;

namespace Logic.Interfaces;

public interface IProductLogic
{
    Product GetProduct(int id);
    List<Product> GetProducts();
    Product AddProduct(Product product);
}