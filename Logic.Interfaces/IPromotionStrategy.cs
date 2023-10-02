using Domain;

namespace Logic.Interfaces;

public interface IPromotionStrategy
{
    public int GetDiscount(List<Product> products);
}