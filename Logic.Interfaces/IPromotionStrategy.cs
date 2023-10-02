using Domain;

namespace Logic.Interfaces;

public interface IPromotionStrategy
{
    public string Name { get; }
    public float GetDiscount(List<Product> products);
}