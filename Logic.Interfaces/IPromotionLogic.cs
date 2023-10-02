using Domain;

namespace Logic.Interfaces;

public interface IPromotionLogic
{
    public IPromotionStrategy GetBestPromotion(List<Product> products);
}