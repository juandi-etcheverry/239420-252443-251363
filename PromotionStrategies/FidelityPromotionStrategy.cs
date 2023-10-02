using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;

public class FidelityPromotionStrategy : IPromotionStrategy
{
    public string Name => "Fidelity Promotion";
    public float GetDiscount(List<Product> products)
    {
        return 0;
    }
}