using Domain;

namespace Logic.Interfaces;

public interface IPurchaseLogic
{
    public Purchase AddProducts(List<PurchaseProduct> products, Purchase purchase);
    public Purchase DeleteProduct(PurchaseProduct product, Purchase purchase);
    public Purchase AddCart(Purchase purchase);
    public void SetFinalPrice(Purchase purchase);
    public List<Purchase> GetAllPurchasesHistory(User user);
    public List<Purchase> GetAllPurchasesHistory();
    public void ValidatePaymentMethod(string method); 
}