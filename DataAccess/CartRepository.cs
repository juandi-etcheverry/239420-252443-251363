using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
namespace DataAccess;

public class CartRepository : ICartRepository
{
    private readonly DbContext _context;
    
    public CartRepository(DbContext context)
    {
        _context = context;
    }
    public Purchase AddCart(Purchase purchase)
    {
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
    /*public Cart GetCartBySession(SessionToken sessionToken)
    {
        //if (sessionToken == null) throw new ArgumentException("SessionToken is null");
        return _context.Set<Cart>().Include(c => c.Products).FirstOrDefault(c => c.Session == sessionToken);
    }*/
}