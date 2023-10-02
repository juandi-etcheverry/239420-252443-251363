using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;

public class TwentyPercentPromotionStrategy : IPromotionStrategy
{
    public int GetDiscount(List<Product> products)
    {
        if (products.Count < 2) return 0;
        return 0;
    }
}