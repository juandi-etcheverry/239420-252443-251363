using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;
/*
 POSSIBLE OPTIMIZATIONS :
    - Use a dictionary to store the colors and a tuple with the amount
    of products and the max price.
 */
public class TotalLookPromotionStrategy : IPromotionStrategy
{
    private const float DiscountPercentage = 0.5f;

    public float GetDiscount(List<Product> products)
    {
        var filteredProducts = FilterValidProductsForPromotion(products);
        if (filteredProducts.Count < 3) return 0;
        return GetFinalDiscountAmount(filteredProducts);
    }

    private List<Product> FilterValidProductsForPromotion(List<Product> products)
    {
        return products.FindAll(p => !p.IsDeleted);
    }

    // Due to how distinct works, the products MUST share the same reference to the colors
    // otherwise, the distinct will not work as expected
    private float GetFinalDiscountAmount(List<Product> products)
    {
        var colors = GetUniqueColorsWithAtLeastThreeProducts(products);
        if (colors.Count == 0) return 0;
        var validProducts = GetValidProducts(products, colors);
        return validProducts.Max(p => p.Price) * DiscountPercentage;
    }

    private List<Color> GetUniqueColorsWithAtLeastThreeProducts(List<Product> products)
    {
        var uniqueColors = products.SelectMany(p => p.Colors).Distinct().ToList();
        return uniqueColors.FindAll(c => products.FindAll(p => p.Colors.Contains(c)).Count >= 3);
    }
    
    private List<Product> GetValidProducts(List<Product> products, List<Color> colors)
    {
        var validProducts = products.FindAll(p => p.Colors.Any(c => colors.Contains(c)));
        return validProducts;
    }

}