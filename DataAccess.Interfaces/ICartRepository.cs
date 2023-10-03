using Domain;
namespace DataAccess.Interfaces;

public interface ICartRepository 
{
    public Purchase AddCart(Purchase purchase);
    public Purchase AddProducts(Purchase purchase, List<Product> product);
    public Purchase DeleteProduct(Purchase purchase, Product product);
}