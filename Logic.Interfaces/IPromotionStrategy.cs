using Domain;

namespace Logic.Interfaces;

public interface IPromotionStrategy
{
    public string Name { get; }
    public bool IsEnabled { get; set; }
    public float GetDiscount(List<Product> products);
}