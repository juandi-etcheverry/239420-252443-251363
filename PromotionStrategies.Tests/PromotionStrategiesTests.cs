using Domain;

namespace PromotionStrategies.Tests;

[TestClass]
public class PromotionStrategiesTests
{
    [TestMethod]
    public void TwentyPercent_EmptyList_OK()
    {
        var strategy = new TwentyPercentPromotionStrategy();
        var result = strategy.GetDiscount(new List<Product>());
        Assert.AreEqual(0, result);
    }
}