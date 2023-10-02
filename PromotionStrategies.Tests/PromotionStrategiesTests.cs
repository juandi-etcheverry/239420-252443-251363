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
    
    [TestMethod]
    public void TwentyPercent_TwoProducts_OK()
    {
        //Arrange
        var strategy = new TwentyPercentPromotionStrategy();
        var product1 = new Product()
        {
            Price = 100,
            Brand = new Brand() { Name = "Brand" },
            Category = new Category() { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 1",
        };
        
        var product2 = new Product()
        {
            Price = 190,
            Brand = new Brand() { Name = "Brand" },
            Category = new Category() { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 2",
        };

        var products = new List<Product>() { product1, product2 };
        
        //Act
        var result = strategy.GetDiscount(products);
        
        //Assert
        Assert.AreEqual(38, result);
    }

    [TestMethod]
    public void TwentyPercent_TwoDeletedProducts_OK()
    {
        //Arrange
        var strategy = new TwentyPercentPromotionStrategy();
        var product1 = new Product()
        {
            Price = 100,
            Brand = new Brand() { Name = "Brand" },
            Category = new Category() { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 1",
            IsDeleted = true
        };

        var product2 = new Product()
        {
            Price = 190,
            Brand = new Brand() { Name = "Brand" },
            Category = new Category() { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 2",
            IsDeleted = true
        };

        var products = new List<Product>() { product1, product2 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void TwentyPercent_DeletedAndNotDeleted_OK()
    {
        //Arrange
        var strategy = new TwentyPercentPromotionStrategy();
        var product1 = new Product()
        {
            Price = 100,
            Brand = new Brand() { Name = "Brand" },
            Category = new Category() { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 1",
            IsDeleted = true
        };

        var product2 = new Product()
        {
            Price = 190,
            Brand = new Brand() { Name = "Brand" },
            Category = new Category() { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 2",
            IsDeleted = true
        };

        var product3 = new Product()
        {
            Price = 300,
            Brand = new Brand() { Name = "Brand" },
            Category = new Category() { Name = "    Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 3",
            IsDeleted = false
        };
        
        var product4 = new Product()
        {
            Price = 400,
            Brand = new Brand() { Name = "Brand" },
            Category = new Category() { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 4",
            IsDeleted = false
        };
        
        var products = new List<Product>() { product1, product2, product3, product4 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(80, result);
    }
    
    [TestMethod]
    public void TotalLook_EmptyList_OK()
    {
        var strategy = new TotalLookPromotionStrategy();
        var result = strategy.GetDiscount(new List<Product>());
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void TotalLook_ThreeProducts_OK()
    {
        //Arrange
        var strategy = new TotalLookPromotionStrategy();
        var product1 = new Product()
        {
            Price = 100,
            Brand = new Brand() { Name = "Brand" },
            Category = new Category() { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 1",
        };
        
        var product2 = new Product()
        {
            Price = 190,
            Brand = new Brand() { Name = "Brand" },
            Category = new Category() { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 2",
        };
        
        var product3 = new Product()
        {
            Price = 300,
            Brand = new Brand() { Name = "Brand" },
            Category = new Category() { Name = "    Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 3",
        };
        
        var products = new List<Product>() { product1, product2, product3 };
        
        //Act
        var result = strategy.GetDiscount(products);
        
        //Assert
        Assert.AreEqual(150, result);
    }
}