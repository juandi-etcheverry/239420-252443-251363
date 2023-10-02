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
        var discount = orderedProducts.TakeWhile((_, idx) => idx < 2).ToList().Sum(p => p.Price);
        return discount;
    }
}