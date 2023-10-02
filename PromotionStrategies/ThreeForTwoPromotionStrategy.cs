using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;

public class ThreeForTwoPromotionStrategy : IPromotionStrategy
{
    private const float DiscountPercentage = 1f;
    public float GetDiscount(List<Product> products)
    {
        if (products.Count < 3) return 0f;
        return products.Min(p => p.Price) * DiscountPercentage;
    }
}