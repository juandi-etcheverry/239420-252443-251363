using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;

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

    private float GetFinalDiscountAmount(List<Product> products)
    {
        return 0;
    }

}