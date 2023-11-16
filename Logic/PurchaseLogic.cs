using DataAccess.Interfaces;
using Domain;
using Logic.Interfaces;

namespace Logic;

public class PurchaseLogic : IPurchaseLogic
{
    private readonly IPromotionLogic _promotionStrategies;
    private readonly IPurchaseRepository _purchaseRepository;
    private static readonly string[] _paymentMethods = new[] { "CreditVisa", "CreditMastercard", "DebitSantander", "DebitItau", "DebitBBVA", "Paypal", "Paganza" };

    public PurchaseLogic(IPurchaseRepository purchaseRepository, IPromotionLogic promotionStrategies)
    {
        _purchaseRepository = purchaseRepository;
        _promotionStrategies = promotionStrategies;
    }

    public PurchaseLogic(IPurchaseRepository purchaseRepository)
    {
        _purchaseRepository = purchaseRepository;
    }

    public Purchase AddProducts(List<PurchaseProduct> products, Purchase purchase)
    {
        var result = _purchaseRepository.AddProducts(purchase, products);
        return result;
    }

    public Purchase DeleteProduct(PurchaseProduct product, Purchase purchase)
    {
        var result = _purchaseRepository.DeleteProduct(purchase, product);
        return result;
    }

    public Purchase AddCart(Purchase purchase)
    {
        SetFinalPrice(purchase);
        var result = _purchaseRepository.AddPurchase(purchase);
        return result;
    }

    public void SetFinalPrice(Purchase purchase)
    {
        try
        {
            var foundProducts = new List<Product>();
            foreach (var p in purchase.Products)
            {
                for (int i = 0; i < p.Quantity; i++)
                {
                    foundProducts.Add(p.Product);
                }
            }
            var promotion = _promotionStrategies.GetBestPromotion(foundProducts);
            var result = promotion.GetDiscount(foundProducts);
           
            purchase.FinalPrice = purchase.TotalPrice - result;
            purchase.PromotionName = promotion.Name;
        }
        catch (ArgumentException)
        {
            purchase.FinalPrice = purchase.TotalPrice;
            purchase.PromotionName = "No Promotion";
        }
        finally
        {
            if (purchase.PaymentMethod == "Paganza") purchase.FinalPrice *= 0.9f;
        }
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

    public void ValidatePaymentMethod(string method)
    {
        if (!_paymentMethods.Contains(method)) throw new ArgumentException("Invalid payment method");
    }
}