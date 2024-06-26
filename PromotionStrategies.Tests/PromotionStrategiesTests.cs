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
        var product1 = new Product
        {
            Price = 100,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 1"
        };

        var product2 = new Product
        {
            Price = 190,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 2"
        };

        var products = new List<Product> { product1, product2 };

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
        var product1 = new Product
        {
            Price = 100,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 1",
            IsDeleted = true
        };

        var product2 = new Product
        {
            Price = 190,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 2",
            IsDeleted = true
        };

        var products = new List<Product> { product1, product2 };

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
        var product1 = new Product
        {
            Price = 100,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 1",
            IsDeleted = true
        };

        var product2 = new Product
        {
            Price = 190,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 2",
            IsDeleted = true
        };

        var product3 = new Product
        {
            Price = 300,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "    Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 3",
            IsDeleted = false
        };

        var product4 = new Product
        {
            Price = 400,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 4",
            IsDeleted = false
        };

        var products = new List<Product> { product1, product2, product3, product4 };

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

        var c1 = new Color { Name = "Red" };
        var c2 = new Color { Name = "Blue" };

        var product1 = new Product
        {
            Price = 100,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c1, c2 },
            Name = "Product 1"
        };

        var product2 = new Product
        {
            Price = 190,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Name = "Product 2",
            Colors = new List<Color> { c1, c2 }
        };

        var product3 = new Product
        {
            Price = 300,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "    Category" },
            Name = "Product 3",
            Colors = new List<Color> { c1, c2 }
        };

        var products = new List<Product> { product1, product2, product3 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(150, result);
    }

    [TestMethod]
    public void TotalLook_DifferentColors_OK()
    {
        //Arrange
        var strategy = new TotalLookPromotionStrategy();
        var c1 = new Color { Name = "Red" };
        var c2 = new Color { Name = "Blue" };
        var c3 = new Color { Name = "Green" };

        var product1 = new Product
        {
            Price = 100,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c1, c2 },
            Name = "Product 1"
        };

        var product2 = new Product
        {
            Price = 190,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c1, c2 },
            Name = "Product 2"
        };

        var product3 = new Product
        {
            Price = 300,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c1 },
            Name = "Product 3"
        };

        var product4 = new Product
        {
            Price = 420,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c3 }
        };

        var products = new List<Product> { product1, product2, product3, product4 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(150, result);
    }

    [TestMethod]
    public void TotalLook_DifferentColorsAndDeleted_OK()
    {
        //Arrange
        var strategy = new TotalLookPromotionStrategy();
        var c1 = new Color { Name = "Red" };
        var c2 = new Color { Name = "Blue" };
        var c3 = new Color { Name = "Green" };

        var product1 = new Product
        {
            Price = 100,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c1, c2 },
            Name = "Product 1",
            IsDeleted = true
        };

        var product2 = new Product
        {
            Price = 190,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c1, c2 },
            Name = "Product 2",
            IsDeleted = true
        };

        var product3 = new Product
        {
            Price = 300,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c1 },
            Name = "Product 3",
            IsDeleted = true
        };

        var product4 = new Product
        {
            Price = 420,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c3 },
            IsDeleted = true
        };

        var products = new List<Product> { product1, product2, product3, product4 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void TotalLook_DifferentColorsAndDeletedAndNotDeleted_OK()
    {
        //Arrange
        var strategy = new TotalLookPromotionStrategy();
        var c1 = new Color { Name = "Red" };
        var c2 = new Color { Name = "Blue" };
        var c3 = new Color { Name = "Green" };

        var product1 = new Product
        {
            Price = 100,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c1, c2 },
            Name = "Product 1",
            IsDeleted = true
        };

        var product2 = new Product
        {
            Price = 190,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c1, c2 },
            Name = "Product 2",
            IsDeleted = true
        };

        var product3 = new Product
        {
            Price = 300,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c1 },
            Name = "Product 3",
            IsDeleted = false
        };

        var product4 = new Product
        {
            Price = 420,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c3 },
            IsDeleted = false
        };

        var products = new List<Product> { product1, product2, product3, product4 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void TotalLook_DifferentColorsLessThanThree_OK()
    {
        //Arrange
        var strategy = new TotalLookPromotionStrategy();
        var c1 = new Color { Name = "Red" };
        var c2 = new Color { Name = "Blue" };
        var c3 = new Color { Name = "Green" };

        var product1 = new Product
        {
            Price = 100,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c2 },
            Name = "Product 1"
        };

        var product2 = new Product
        {
            Price = 190,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c1, c2 },
            Name = "Product 2"
        };

        var product3 = new Product
        {
            Price = 300,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c1 },
            Name = "Product 3"
        };

        var product4 = new Product
        {
            Price = 420,
            Brand = new Brand { Name = "Brand" },
            Category = new Category { Name = "Category" },
            Colors = new List<Color> { c3 }
        };

        var products = new List<Product> { product1, product2, product3, product4 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void ThreeForTwo_EmptyList_OK()
    {
        var strategy = new ThreeForTwoPromotionStrategy();
        var result = strategy.GetDiscount(new List<Product>());
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void ThreeForTwo_ThreeProducts_OK()
    {
        //Arrange
        var strategy = new ThreeForTwoPromotionStrategy();
        var c1 = new Category { Name = "Shoe" };

        var product1 = new Product
        {
            Price = 100,
            Brand = new Brand { Name = "Brand" },
            Category = c1,
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 1"
        };

        var product2 = new Product
        {
            Price = 190,
            Brand = new Brand { Name = "Brand" },
            Category = c1,
            Name = "Product 2",
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
        };

        var product3 = new Product
        {
            Price = 300,
            Brand = new Brand { Name = "Brand" },
            Category = c1,
            Name = "Product 3",
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
        };

        var products = new List<Product> { product1, product2, product3 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(100, result);
    }


    [TestMethod]
    public void ThreeForTwo_DifferentCategoriesNoPromo_OK()
    {
        //Arrange
        var strategy = new ThreeForTwoPromotionStrategy();
        var c1 = new Category { Name = "Shoe" };
        var c2 = new Category { Name = "Shirt" };

        var product1 = new Product
        {
            Price = 100,
            Brand = new Brand { Name = "Brand" },
            Category = c2,
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 1"
        };

        var product2 = new Product
        {
            Price = 190,
            Brand = new Brand { Name = "Brand" },
            Category = c1,
            Name = "Product 2",
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
        };

        var product3 = new Product
        {
            Price = 300,
            Brand = new Brand { Name = "Brand" },
            Category = c2,
            Name = "Product 3",
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
        };

        var products = new List<Product> { product1, product2, product3 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void ThreeForTwo_DifferentCategoriesWithPromo_OK()
    {
        //Arrange
        var strategy = new ThreeForTwoPromotionStrategy();
        var c1 = new Category { Name = "Shoe" };
        var c2 = new Category { Name = "Shirt" };

        var product1 = new Product
        {
            Price = 100,
            Brand = new Brand { Name = "Brand" },
            Category = c2,
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 1"
        };

        var product2 = new Product
        {
            Price = 190,
            Brand = new Brand { Name = "Brand" },
            Category = c1,
            Name = "Product 2",
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
        };

        var product3 = new Product
        {
            Price = 300,
            Brand = new Brand { Name = "Brand" },
            Category = c2,
            Name = "Product 3",
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
        };
        var product4 = new Product
        {
            Price = 420,
            Brand = new Brand { Name = "Brand" },
            Category = c1,
            Name = "Product 3",
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
        };
        var product5 = new Product
        {
            Price = 420,
            Brand = new Brand { Name = "Brand" },
            Category = c1,
            Name = "Product 5",
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
        };

        var products = new List<Product> { product1, product2, product3, product4, product5 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(190, result);
    }

    [TestMethod]
    public void ThreeForTwo_DifferentCategoriesDeleted_OK()
    {
        //Arrange
        var strategy = new ThreeForTwoPromotionStrategy();
        var c1 = new Category { Name = "Shoe" };
        var c2 = new Category { Name = "Shirt" };

        var product1 = new Product
        {
            Price = 100,
            Brand = new Brand { Name = "Brand" },
            Category = c2,
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 1"
        };

        var product2 = new Product
        {
            Price = 190,
            Brand = new Brand { Name = "Brand" },
            Category = c1,
            Name = "Product 2",
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
        };

        var product3 = new Product
        {
            Price = 300,
            Brand = new Brand { Name = "Brand" },
            Category = c2,
            Name = "Product 3",
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
        };
        var product4 = new Product
        {
            Price = 420,
            Brand = new Brand { Name = "Brand" },
            Category = c1,
            Name = "Product 3",
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
        };
        var product5 = new Product
        {
            Price = 420,
            Brand = new Brand { Name = "Brand" },
            Category = c1,
            Name = "Product 5",
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            IsDeleted = true
        };

        var products = new List<Product> { product1, product2, product3, product4, product5 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Fidelity_EmptyList_OK()
    {
        //Arrange
        var strategy = new FidelityPromotionStrategy();

        //Act
        var result = strategy.GetDiscount(new List<Product>());

        //Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Fidelity_ThreeProducts_OK()
    {
        //Arrange
        var strategy = new FidelityPromotionStrategy();
        var b1 = new Brand
        {
            Name = "Brand"
        };

        var product1 = new Product
        {
            Price = 100,
            Brand = b1,
            Category = new Category { Name = "    Category" },
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } },
            Name = "Product 1"
        };

        var product2 = new Product
        {
            Price = 190,
            Brand = b1,
            Category = new Category { Name = "    Category" },
            Name = "Product 2",
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
        };

        var product3 = new Product
        {
            Price = 300,
            Brand = b1,
            Category = new Category { Name = "    Category" },
            Name = "Product 3",
            Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
        };

        var products = new List<Product> { product1, product2, product3 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(290, result);
    }

    [TestMethod]
    public void Fidelity_DifferentBrands_OK()
    {
        //Arrange
        var strategy = new FidelityPromotionStrategy();

        var b1 = new Brand
        {
            Name = "Gucci"
        };
        var b2 = new Brand
        {
            Name = "Nike"
        };

        var p1 = new Product
        {
            Name = "P1",
            Price = 10000,
            Brand = b1,
            Colors = new List<Color> { new() { Name = "Blue" } },
            Category = new Category { Name = "Bag" },
            Description = "A luxury Gucci Bag"
        };

        var p2 = new Product
        {
            Name = "P2",
            Price = 90,
            Brand = b2,
            Colors = new List<Color> { new() { Name = "Blue" } },
            Category = new Category { Name = "Shoe" },
            Description = "Nike Sneakers"
        };

        var p3 = new Product
        {
            Name = "P3",
            Price = 250,
            Brand = b1,
            Colors = new List<Color> { new() { Name = "Blue" } },
            Category = new Category { Name = "Shoe" },
            Description = "Collection Nikeys"
        };

        var products = new List<Product> { p1, p2, p3 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Fidelity_DifferentBrandsWithPromo_OK()
    {
        //Arrange
        var strategy = new FidelityPromotionStrategy();

        var b1 = new Brand
        {
            Name = "Gucci"
        };
        var b2 = new Brand
        {
            Name = "Nike"
        };

        var p1 = new Product
        {
            Name = "P1",
            Price = 10,
            Brand = b1,
            Colors = new List<Color> { new() { Name = "Blue" } },
            Category = new Category { Name = "Bag" },
            Description = "A ripoff Gucci Bag"
        };

        var p2 = new Product
        {
            Name = "P2",
            Price = 90,
            Brand = b2,
            Colors = new List<Color> { new() { Name = "Blue" } },
            Category = new Category { Name = "Shoe" },
            Description = "Nike Sneakers"
        };

        var p3 = new Product
        {
            Name = "P3",
            Price = 250,
            Brand = b2,
            Colors = new List<Color> { new() { Name = "Blue" } },
            Category = new Category { Name = "Shoe" },
            Description = "Collection Nikeys"
        };

        var p4 = new Product
        {
            Name = "P4",
            Price = 100,
            Brand = b2,
            Colors = new List<Color> { new() { Name = "Blue" } },
            Category = new Category { Name = "Sandal" },
            Description = "Nike sandals"
        };

        var products = new List<Product> { p1, p2, p3, p4 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(190, result);
    }

    [TestMethod]
    public void Fidelity_DifferentBrandsDeleted_OK()
    {
        //Arrange
        var strategy = new FidelityPromotionStrategy();

        var b1 = new Brand
        {
            Name = "Gucci"
        };
        var b2 = new Brand
        {
            Name = "Nike"
        };

        var p1 = new Product
        {
            Name = "P1",
            Price = 10,
            Brand = b1,
            Colors = new List<Color> { new() { Name = "Blue" } },
            Category = new Category { Name = "Bag" },
            Description = "A ripoff Gucci Bag"
        };

        var p2 = new Product
        {
            Name = "P2",
            Price = 90,
            Brand = b2,
            Colors = new List<Color> { new() { Name = "Blue" } },
            Category = new Category { Name = "Shoe" },
            Description = "Nike Sneakers",
            IsDeleted = true
        };

        var p3 = new Product
        {
            Name = "P3",
            Price = 250,
            Brand = b2,
            Colors = new List<Color> { new() { Name = "Blue" } },
            Category = new Category { Name = "Shoe" },
            Description = "Collection Nikeys"
        };

        var p4 = new Product
        {
            Name = "P4",
            Price = 100,
            Brand = b2,
            Colors = new List<Color> { new() { Name = "Blue" } },
            Category = new Category { Name = "Sandal" },
            Description = "Nike sandals"
        };

        var products = new List<Product> { p1, p2, p3, p4 };

        //Act
        var result = strategy.GetDiscount(products);

        //Assert
        Assert.AreEqual(0, result);
    }
}