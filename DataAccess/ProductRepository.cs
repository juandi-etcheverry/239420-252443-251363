using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ProductRepository : IProductRepository
{
    private readonly DbContext _context;
    
    public ProductRepository(DbContext context)
    {
        _context = context;
    }

    public Product AddProduct(Product product)
    {
        _context.Set<Product>().Add(product);
        _context.SaveChanges();
        return product;
    }
    
    public Product GetProduct(Guid id)
    {
        var product = _context.Set<Product>().Find(id);
        if (product == null) throw new ArgumentException($"Product with id {id} not found");
        return product;
    }
    
    public List<Product> GetProducts()
    {
        return _context.Set<Product>().ToList();
    }
    
    public List<Product> GetProducts(Func<Product, bool> predicate)
    {
        return _context.Set<Product>().Where(predicate).ToList();
    }

    public Product SoftDelete(Guid id)
    {
        var product = _context.Set<Product>().Find(id);
        if (product == null) throw new ArgumentException($"Product with id {id} not found");
        product.IsDeleted = true;
        _context.SaveChanges();
        return product;
    }

}