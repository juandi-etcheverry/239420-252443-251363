using Domain;
using Logic.Interfaces;

namespace Logic;

public class PromotionLogic : IPromotionLogic
{
    private IEnumerable<IPromotionStrategy> _promotionStrategies;
    public PromotionLogic(IEnumerable<IPromotionStrategy> promotionStrategies)
    {
        _promotionStrategies = promotionStrategies;
    }
    public IPromotionStrategy GetBestPromotion(List<Product> products)
    {
        var bestPromotion = _promotionStrategies.MaxBy(st => st.GetDiscount(products));
        if (bestPromotion == null || bestPromotion.GetDiscount(products) <= 0)
        {
            throw new ArgumentException("No promotion is applicable to these products");
        }
        return bestPromotion;
    }
}