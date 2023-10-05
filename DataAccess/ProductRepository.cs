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
        var category = _context.Set<Category>().Find(product.Category.Id);
        var brand = _context.Set<Brand>().Find(product.Brand.Id);
        var colors = _context.Set<Color>().Where(c => product.Colors.Select(pc => pc.Id).Contains(c.Id)).ToList();
        product.Category = category;
        product.Brand = brand;
        product.Colors = colors;
        _context.Set<Product>().Add(product);
        _context.SaveChanges();
        return product;
    }

    public Product GetProduct(Guid id)
    {
        var product = _context.Set<Product>()
            .Include(p => p.Brand)
            .Include(pp => pp.Category)
            .Include(ppp => ppp.Colors)
            .FirstOrDefault(x => x.Id == id);
        if (product == null) throw new ArgumentException($"Product with id {id} not found");
        return product;
    }

    public List<Product> GetProducts()
    {
        return _context.Set<Product>().Include(p => p.Brand)
            .Include(pp => pp.Category)
            .Include(ppp => ppp.Colors)
            .ToList();
    }

    public List<Product> GetProducts(Func<Product, bool> predicate)
    {
        return _context.Set<Product>().Include(p => p.Brand)
            .Include(pp => pp.Category)
            .Include(ppp => ppp.Colors)
            .Where(predicate)
            .ToList();
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