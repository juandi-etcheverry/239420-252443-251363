using Domain;

namespace DataAccess.Interfaces;

public interface IPurchaseRepository
{
    public Purchase AddPurchase(Purchase purchase);
    public Purchase AddProducts(Purchase purchase, List<PurchaseProduct> product);
    public Purchase DeleteProduct(Purchase purchase, PurchaseProduct product);
    public List<Purchase> GetAllPurchasesHistory(User user);
    public List<Purchase> GetAllPurchasesHistory();
}