using Domain;
using Logic.Interfaces;
using Moq;
using System;
using System.Configuration;

namespace Logic.Tests;

[TestClass]
public class PromotionLogicTest
{
    private Mock<IFileDataReader> mock;
    private const string _directoryPath = "../../../../PromotionsDllFiles";
   
    [TestInitialize]
    public void TestInitialize()
    {
        var paths = new FileDataReader().GetDirectoryFilePaths(_directoryPath);

        mock = new Mock<IFileDataReader>(MockBehavior.Strict);
        mock.Setup(fr => fr.GetDirectoryFilePaths(It.IsAny<string>())).Returns(paths);
        mock.Setup(fr => fr.GetLastModified(It.IsAny<string>())).Returns(DateTime.Now);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "No promotion is applicable to these products")]
    public void NoBestPromotion_FAIL()
    {
        var products = new List<Product>();
        var logic = new PromotionLogic(mock.Object);

        var result = logic.GetBestPromotion(products);
    }

    [TestMethod]
    public void BestPromotionOutOfTwo_OK()
    {
        var b1 = new Brand { Name = "ORT Merch" };
        var c1 = new Category { Name = "T-Shirt" };

        var products = new List<Product>
        {
            new() { Brand = b1, Category = c1, Price = 100 },
            new() { Brand = b1, Category = c1, Price = 200 },
            new() { Brand = b1, Category = c1, Price = 300 },
            new() { Brand = b1, Category = c1, Price = 400 },
            new() { Brand = b1, Category = c1, Price = 500 }
        };

        var logic = new PromotionLogic(mock.Object);
        
        var result = logic.GetBestPromotion(products);
        
        Assert.AreEqual("Fidelity Promotion", result.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "No promotion is applicable to these products")]
    public void BestPromotion_ProductsDontApply_FAIL()
    {
        var b1 = new Brand { Name = "ORT Merch" };
        var c1 = new Category { Name = "T-Shirt" };

        var products = new List<Product>
        {
            new() { Brand = b1, Category = c1, Price = 100, PromotionsApply = false },
            new() { Brand = b1, Category = c1, Price = 200, PromotionsApply = false },
            new() { Brand = b1, Category = c1, Price = 300, PromotionsApply = false },
            new() { Brand = b1, Category = c1, Price = 400, PromotionsApply = false },
            new() { Brand = b1, Category = c1, Price = 500, PromotionsApply = false }
        };

        var logic = new PromotionLogic(mock.Object);
        
        var result = logic.GetBestPromotion(products);
    }
}