using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;

public class ThreeForTwoPromotionStrategy : IPromotionStrategy
{
    private const float DiscountPercentage = 1f;
    public float GetDiscount(List<Product> products)
    {
        var filteredProducts = products.FindAll(p => !p.IsDeleted);
        if (filteredProducts.Count < 3) return 0f;
        var uniqueCategories = filteredProducts.Select(p => p.Category).Distinct().ToList();
        var categoriesWithAtLeastThreeProducts = uniqueCategories.FindAll(c => filteredProducts.FindAll(p => p.Category == c).Count >= 3);
        if (categoriesWithAtLeastThreeProducts.Count == 0) return 0f;
        var validProducts = filteredProducts.FindAll(p => categoriesWithAtLeastThreeProducts.Contains(p.Category));
        return validProducts.Min(p => p.Price) * DiscountPercentage;
    }
}