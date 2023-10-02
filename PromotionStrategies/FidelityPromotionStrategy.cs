using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;

public class FidelityPromotionStrategy : IPromotionStrategy
{
    public string Name => "Fidelity Promotion";
    public float GetDiscount(List<Product> products)
    {
        if (products.Count < 3) return 0;
        var orderedProducts = products.OrderBy(p => p.Price).ToList();
        var uniqueBrands = orderedProducts.Select(p => p.Brand).Distinct().ToList();
        var brandsWithThreeProducts = uniqueBrands.FindAll(b => orderedProducts.FindAll(p => p.Brand == b).Count >= 3);
        var filteredProducts = orderedProducts.TakeWhile(p => brandsWithThreeProducts.Contains(p.Brand));
        var discount = filteredProducts.TakeWhile((_, idx) => idx < 2).ToList().Sum(p => p.Price);
        return discount;
    }
}