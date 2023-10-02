using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;

public class TwentyPercentPromotionStrategy : IPromotionStrategy
{
    public float GetDiscount(List<Product> products)
    {
        var filteredProducts = products.FindAll(p => !p.IsDeleted);
        if (filteredProducts.Count < 2) return 0;
        var discountPercentage = 20f / 100f;
        return filteredProducts.Max(p => p.Price) * discountPercentage;
    }
}