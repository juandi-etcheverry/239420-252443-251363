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
    public Cart AddCart(Cart cart)
    {
        if (cart.Session == null) throw new ArgumentException("Session is null");
        _context.Set<Cart>().Add(cart);
        _context.SaveChanges();
        return cart;
    }
    public Cart AddProduct(Cart cart, Product product)
    {
        if (cart == null) throw new ArgumentException("Cart is null");
        if (product == null) throw new ArgumentException("Product is null");
        cart.AddProduct(product);
        _context.SaveChanges();
        return cart;
    }
}