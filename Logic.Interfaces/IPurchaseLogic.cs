using Domain;
namespace Logic.Interfaces;

public interface IPurchaseLogic
{
    public Purchase AddProduct(Product product, Purchase purchase);
    public Purchase DeleteProduct(Product product, Purchase purchase);
    public Purchase AddCart(Purchase purchase);
}