using Domain;
using Logic.Interfaces;
using Moq;
using PromotionStrategies;

namespace Logic.Tests;

[TestClass]
public class PromotionLogicTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "No promotion is applicable to these products")]
    public void NoBestPromotion_FAIL()
    {
        var products = new List<Product>();
        var strat1 = new FidelityPromotionStrategy();
        var strat2 = new TwentyPercentPromotionStrategy();
        var strat3 = new ThreeForTwoPromotionStrategy();
        var strat4 = new TotalLookPromotionStrategy();
        
        var logic = new PromotionLogic(new List<IPromotionStrategy> {strat1, strat2, strat3, strat4});
        
        var result = logic.GetBestPromotion(products);
    }

    [TestMethod]
    public void BestPromotionOutOfTwo_OK()
    {
        var b1 = new Brand() { Name = "ORT Merch" };
        var c1 = new Category() {Name = "T-Shirt"};
        
        var products = new List<Product>()
        {
            new Product() {Brand = b1, Category = c1, Price = 100},
            new Product() {Brand = b1, Category = c1, Price = 200},
            new Product() {Brand = b1, Category = c1, Price = 300},
            new Product() {Brand = b1, Category = c1, Price = 400},
            new Product() {Brand = b1, Category = c1, Price = 500},
        };
        
        var strat1 = new FidelityPromotionStrategy();
        var strat2 = new TwentyPercentPromotionStrategy();
        var strat3 = new ThreeForTwoPromotionStrategy();
        var strat4 = new TotalLookPromotionStrategy();

        var logic = new PromotionLogic(new List<IPromotionStrategy> { strat1, strat2, strat3, strat4 });

        var result = logic.GetBestPromotion(products);

        Assert.AreEqual(strat1, result);
    }
}