using Domain;

namespace Logic.Interfaces;

public interface IPromotionStrategy
{
    public float GetDiscount(List<Product> products);
}