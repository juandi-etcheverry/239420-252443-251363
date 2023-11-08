using Domain;

namespace Logic.Interfaces;

public interface IPromotionLogic
{
    public IPromotionStrategy GetBestPromotion(List<Product> products);
    public List<IPromotionStrategy> GetAllPromotions();
    public void ForceRefresh();
    public bool TogglePromotion(string name);
}