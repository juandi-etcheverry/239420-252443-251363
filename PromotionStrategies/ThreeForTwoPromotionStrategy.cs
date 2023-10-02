using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;

public class ThreeForTwoPromotionStrategy : IPromotionStrategy
{
    private const float DiscountPercentage = 1f;
    public float GetDiscount(List<Product> products)
    {
        if (products.Count < 3) return 0f;
        var uniqueCategories = products.Select(p => p.Category).Distinct().ToList();
        var categoriesWithAtLeastThreeProducts = uniqueCategories.FindAll(c => products.FindAll(p => p.Category == c).Count >= 3);
        if (categoriesWithAtLeastThreeProducts.Count == 0) return 0f;
        var validProducts = products.FindAll(p => categoriesWithAtLeastThreeProducts.Contains(p.Category));
        return validProducts.Min(p => p.Price) * DiscountPercentage;
    }
}