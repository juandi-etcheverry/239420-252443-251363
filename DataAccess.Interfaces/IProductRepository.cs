using Domain;

namespace DataAccess.Interfaces;

public interface IProductRepository
{
    public Product AddProduct(Product product);
}