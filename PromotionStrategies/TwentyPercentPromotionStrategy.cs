using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;

public class TwentyPercentPromotionStrategy : IPromotionStrategy
{
    public float GetDiscount(List<Product> products)
    {
        if (products.Count < 2) return 0;
        var discountPercentage = 20f / 100f;
        return products.Max(x => x.Price) * discountPercentage;
    }
}