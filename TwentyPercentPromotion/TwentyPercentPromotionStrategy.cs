using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;

public class TwentyPercentPromotionStrategy : IPromotionStrategy
{
    private const float DiscountPercentage = 0.2f;
    public string Name => "20% Promotion";

    public float GetDiscount(List<Product> products)
    {
        var filteredProducts = FilterValidProductsForPromotion(products);
        if (filteredProducts.Count < 2) return 0;
        return GetFinalDiscountAmount(filteredProducts);
    }

    private List<Product> FilterValidProductsForPromotion(List<Product> products)
    {
        return products.FindAll(p => !p.IsDeleted);
    }

    private float GetFinalDiscountAmount(List<Product> products)
    {
        return products.Max(p => p.Price) * DiscountPercentage;
    }
}