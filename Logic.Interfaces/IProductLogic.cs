using Domain;

namespace Logic.Interfaces;

public interface IProductLogic
{
    Product GetProduct(Guid id);
    List<Product> GetProducts();
    List<Product> GetProducts(Func<Product, bool> predicate);
    Product AddProduct(Product product);
    Product SoftDelete(Guid id);
    public Product DecreaseStock(Guid id, int quantity);
    public void IsPurchaseValid(IList<PurchaseProduct> cart);
    public Product UpdateProduct(Guid id, Product product);
}