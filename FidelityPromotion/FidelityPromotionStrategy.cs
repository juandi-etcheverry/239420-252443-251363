using Domain;
using Logic.Interfaces;

namespace PromotionStrategies;

public class FidelityPromotionStrategy : IPromotionStrategy
{
    public string Name => "Fidelity Promotion";

    public float GetDiscount(List<Product> products)
    {
        var validProducts = products.FindAll(p => !p.IsDeleted);
        if (validProducts.Count < 3) return 0;
        var orderedProducts = OrderedProductsValidForPromotion(validProducts);
        var discount = orderedProducts.TakeWhile((_, idx) => idx < 2).ToList().Sum(p => p.Price);
        return discount;
    }

    private List<Product> OrderedProductsValidForPromotion(List<Product> validProducts)
    {
        var brandsWithThreeProducts = BrandsWithThreeProducts(validProducts);
        var filteredProducts = validProducts.FindAll(p => brandsWithThreeProducts.Contains(p.Brand));
        var orderedProducts = filteredProducts.OrderBy(p => p.Price);
        return orderedProducts.ToList();
    }

    private List<Brand> BrandsWithThreeProducts(List<Product> validProducts)
    {
        var uniqueBrands = validProducts.Select(p => p.Brand).Distinct().ToList();
        var brandsWithThreeProducts = uniqueBrands.FindAll(b => validProducts.FindAll(p => p.Brand == b).Count >= 3);
        return brandsWithThreeProducts;
    }
}