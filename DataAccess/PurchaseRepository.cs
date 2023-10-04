using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
namespace DataAccess;

public class PurchaseRepository : IPurchaseRepository
{
    private readonly DbContext _context;
    
    public PurchaseRepository(DbContext context)
    {
        _context = context;
    }
    public Purchase AddPurchase(Purchase purchase)
    {
        List<Product> products = _context.Set<Product>().Where(p => purchase.Products.Select(pp => pp.Id).Contains(p.Id)).ToList();
        User user = _context.Set<User>().Find(purchase.User.Id);
        purchase.Products = products;
        purchase.User = user;
        _context.Set<Purchase>().Add(purchase);
        _context.SaveChanges();
        return purchase;
    }
    public Purchase AddProducts(Purchase purchase, List<Product> products)
    {
        if (purchase == null) throw new ArgumentException("Cart is null");
        if (products.Count == 0) throw new ArgumentException("There are no products to Add");
        purchase.AddProducts(products);
        _context.SaveChanges();
        return purchase;
    }
    public Purchase DeleteProduct(Purchase purchase, Product product)
    {
        if (purchase == null) throw new ArgumentException("Cart is null");
        if (product == null) throw new ArgumentException("Product is null");
        purchase.DeleteProduct(product);
        _context.SaveChanges();
        return purchase;
    }

    public Purchase AssignUserToPurchase(Purchase purchase, User user)
    {
        if (user == null) throw new ArgumentException("User is null");
        purchase.AssignUser(user);
        return purchase;
    }

    public List<Purchase> GetAllPurchasesHistory(User user)
    {
        if(user == null) throw new ArgumentException("User is null");
        var result = _context.Set<Purchase>().Include(p => p.Products).Where(p => p.User == user).ToList();
        if(result.Count == 0) throw new ArgumentException("There are no purchases");
        return result;
    }

    public List<Purchase> GetAllPurchasesHistory()
    {
        var result = _context.Set<Purchase>().Include(p => p.Products).ToList();
        if (result.Count == 0) throw new ArgumentException("There are no purchases");
        return result;
    }
}