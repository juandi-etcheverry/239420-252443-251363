using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;

public class FidelityPromotionStrategy : IPromotionStrategy
{
    public string Name => "Fidelity Promotion";
    public float GetDiscount(List<Product> products)
    {
        if (products.Count < 3) return 0;
        var uniqueBrands = products.Select(p => p.Brand).Distinct().ToList();
        var brandsWithThreeProducts = uniqueBrands.FindAll(b => products.FindAll(p => p.Brand == b).Count >= 3);
        var filteredProducts = products.FindAll(p => brandsWithThreeProducts.Contains(p.Brand));
        var orderedProducts = filteredProducts.OrderBy(p => p.Price);
        var discount = orderedProducts.TakeWhile((_, idx) => idx < 2).ToList().Sum(p => p.Price);
        return discount;
    }
}