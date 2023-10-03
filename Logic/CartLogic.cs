using DataAccess.Interfaces;
using Domain;
using Logic.Interfaces;
namespace Logic;

public class CartLogic : ICartLogic
{
    private ICartRepository _cartRepository;
    
    public CartLogic(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }
    public Cart AddProduct(Product product, Cart cart)
    {
        var result = _cartRepository.AddProduct(cart, product);
        return result;
    }
    public Cart DeleteProduct(Product product, Cart cart)
    {
        var result = _cartRepository.DeleteProduct(cart, product);
        return result;
    }
    public Cart AddCart(Cart cart)
    {
        var result = _cartRepository.AddCart(cart);
        return result;
    }
}