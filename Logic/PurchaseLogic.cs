using DataAccess.Interfaces;
using Domain;
using Logic.Interfaces;
namespace Logic;

public class PurchaseLogic : IPurchaseLogic
{
    private ICartRepository _cartRepository;
    
    public PurchaseLogic(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }
    public Purchase AddProduct(Product product, Purchase purchase)
    {
        var products = new List<Product>();
        products.Add(product);
        var result = _cartRepository.AddProducts(purchase, products);
        return result;
    }
    public Purchase DeleteProduct(Product product, Purchase purchase)
    {
        var result = _cartRepository.DeleteProduct(purchase, product);
        return result;
    }
    public Purchase AddCart(Purchase purchase)
    {
        var result = _cartRepository.AddCart(purchase);
        return result;
    }
}