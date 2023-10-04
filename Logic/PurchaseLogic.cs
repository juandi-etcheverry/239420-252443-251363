using DataAccess.Interfaces;
using Domain;
using Logic.Interfaces;
namespace Logic;

public class PurchaseLogic : IPurchaseLogic
{
    private IPurchaseRepository _purchaseRepository;
    private IPromotionLogic _promotionStrategies;
    
    public PurchaseLogic(IPurchaseRepository purchaseRepository, IPromotionLogic promotionStrategies)
    {
        _purchaseRepository = purchaseRepository;
        _promotionStrategies = promotionStrategies;
    }
    public PurchaseLogic(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }
    
    public Purchase AddProducts(List<Product> products, Purchase purchase)
    {
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
        var result = _purchaseRepository.AddPurchase(purchase);
        return result;
    }
    
    public void SetFinalPrice(Purchase purchase)
    {
        var promotion = _promotionStrategies.GetBestPromotion(purchase.Products);
        var result = promotion.GetDiscount(purchase.Products);
        purchase.FinalPrice = purchase.TotalPrice - result;
        purchase.PromotionName = promotion.Name;
    }
    public List<Purchase> GetAllPurchasesHistory(User user)
    {
        var result = _purchaseRepository.GetAllPurchasesHistory(user);
        return result;
    }

    public List<Purchase> GetAllPurchasesHistory()
    {
        return _purchaseRepository.GetAllPurchasesHistory();
    }
}