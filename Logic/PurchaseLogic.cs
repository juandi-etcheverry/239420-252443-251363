using DataAccess.Interfaces;
using Domain;
using Logic.Interfaces;
namespace Logic;

public class PurchaseLogic : IPurchaseLogic
{
    private IPurchaseRepository _purchaseRepository;
    
    public PurchaseLogic(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }
    public Purchase AddProduct(Product product, Purchase purchase)
    {
        var products = new List<Product>();
        products.Add(product);
        var result = _purchaseRepository.AddProducts(purchase, products);
        return result;
    }
    public Purchase DeleteProduct(Product product, Purchase purchase)
    {
        var result = _purchaseRepository.DeleteProduct(purchase, product);
        return result;
    }
    public Purchase AddCart(Purchase purchase)
    {
        var result = _purchaseRepository.AddCart(purchase);
        return result;
    }
}