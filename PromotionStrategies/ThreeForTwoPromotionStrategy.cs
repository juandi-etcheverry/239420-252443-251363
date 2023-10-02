using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;

public class ThreeForTwoPromotionStrategy : IPromotionStrategy
{
    public float GetDiscount(List<Product> products)
    {
        return 0;
    }
}