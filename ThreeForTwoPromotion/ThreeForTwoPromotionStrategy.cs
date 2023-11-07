using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;

public class ThreeForTwoPromotionStrategy : IPromotionStrategy
{
    private const float DiscountPercentage = 1f;
    public string Name => "3x2 Category Promotion";

    public float GetDiscount(List<Product> products)
    {
        var filteredProducts = products.FindAll(p => !p.IsDeleted);
        if (filteredProducts.Count < 3) return 0f;
        var categories = CategoriesWithAtLeastThreeProducts(filteredProducts);
        if (categories.Count == 0) return 0f;
        return GetDiscountAmount(filteredProducts, categories);
    }

    private float GetDiscountAmount(List<Product> filteredProducts, List<Category> categories)
    {
        var validProducts = filteredProducts.FindAll(p => categories.Contains(p.Category));
        return validProducts.Min(p => p.Price) * DiscountPercentage;
    }

    private List<Category> CategoriesWithAtLeastThreeProducts(List<Product> filteredProducts)
    {
        var uniqueCategories = filteredProducts.Select(p => p.Category).Distinct().ToList();
        var categoriesWithAtLeastThreeProducts =
            uniqueCategories.FindAll(c => filteredProducts.FindAll(p => p.Category == c).Count >= 3);
        return categoriesWithAtLeastThreeProducts;
    }
}