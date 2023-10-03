using Domain;
namespace DataAccess.Interfaces;

public interface ICartRepository 
{
    public Cart AddCart(Cart cart);
    public Cart AddProduct(Cart cart, Product product);
    public Cart DeleteProduct(Cart cart, Product product);
}