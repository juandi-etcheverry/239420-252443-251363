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
    
    public Product GetProduct(int id)
    {
        var product = _productRepository.GetProduct(id);
        if (product.IsDeleted) throw new ArgumentException($"Product with id {id} not found");
        return product;
    }
    
    public List<Product> GetProducts()
    {
        return _productRepository.GetProducts(p => p.IsDeleted == false);
    }
    
}