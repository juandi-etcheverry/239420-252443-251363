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
        return _productRepository.GetProduct(id);
    }
    
}