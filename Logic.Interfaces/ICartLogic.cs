using Domain;
namespace Logic.Interfaces;

public interface ICartLogic
{
    public Cart AddProduct(Product product, Cart cart);
    public Cart DeleteProduct(Product product, Cart cart);
    public Cart AddCart(Cart cart);
}