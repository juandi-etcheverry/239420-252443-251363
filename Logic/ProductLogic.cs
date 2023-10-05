using DataAccess.Interfaces;
using Domain;
using Logic.Interfaces;

namespace Logic;

public class ProductLogic : IProductLogic
{
    private IProductRepository _productRepository;
    
    public ProductLogic(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public Product GetProduct(Guid id)
    {
        var product = _productRepository.GetProduct(id);
        if (product.IsDeleted) throw new ArgumentException($"Product with id {id} not found");
        return product;
    }
    
    public List<Product> GetProducts()
    {
        return _productRepository.GetProducts(p => p.IsDeleted == false);
    }

    public Product AddProduct(Product product)
    {
        return _productRepository.AddProduct(product);
    }

    public List<Product> GetProducts(Func<Product, bool> predicate)
    {
        return _productRepository.GetProducts(predicate);
    }

    public Product SoftDelete(Guid id)
    {
        return _productRepository.SoftDelete(id);
    }
}