using DataAccess.Interfaces;
using Domain;
using Moq;

namespace Logic.Tests;

[TestClass]
public class ProductLogicTest
{
    [TestMethod]
    public void GetProduct_ValidId_OK()
    {
        // Arrange
        var product = new Product { Name = "Test", Price = 420, Description = "Test Description" };
        var mock = new Mock<IProductRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetProduct(product.Id)).Returns(product);
        var logic = new ProductLogic(mock.Object);

        // Act
        var result = logic.GetProduct(product.Id);

        // Assert
        Assert.AreEqual(product, result);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Product with id 0 not found")]
    public void GetProduct_InvalidId_FAIL()
    {
        // Arrange
        var product = new Product { Name = "Test", Price = 420, Description = "Test Description" };
        var mock = new Mock<IProductRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetProduct(product.Id)).Throws(new ArgumentException("Product with id 0 not found"));
        var logic = new ProductLogic(mock.Object);

        // Act
        var result = logic.GetProduct(product.Id);

        // Assert
        // Exception
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Product with id 1 not found")]
    public void GetProduct_SoftDeleted_FAIL()
    {
        // Arrange
        var product = new Product { Name = "Test", Price = 420, Description = "Test Description", IsDeleted = true };
        var mock = new Mock<IProductRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetProduct(product.Id)).Returns(product);
        var logic = new ProductLogic(mock.Object);

        // Act
        var result = logic.GetProduct(product.Id);

        // Exception
    }

    [TestMethod]
    public void GetProducts_Valid_OK()
    {
        // Arrange
        var products = new List<Product>
        {
            new() { Name = "Test", Price = 420, Description = "Test Description" },
            new() { Name = "Test2", Price = 69, Description = "Test Description2" }
        };
        var mock = new Mock<IProductRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetProducts(It.IsAny<Func<Product, bool>>())).Returns(products);
        var logic = new ProductLogic(mock.Object);

        // Act
        var result = logic.GetProducts();

        // Assert
        Assert.AreEqual(products, result);
    }

    [TestMethod]
    public void GetProducts_SoftDeleted_OK()
    {
        // Arrange
        var p1 = new Product { Name = "Test", Price = 420, Description = "Test Description" };
        var p2 = new Product { Name = "Test2", Price = 69, Description = "Test Description2", IsDeleted = true };
        var products = new List<Product>
        {
            p1,
            p2
        };
        var mock = new Mock<IProductRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetProducts(It.IsAny<Func<Product, bool>>()))
            .Returns(products);
        var logic = new ProductLogic(mock.Object);

        // Act
        var result = logic.GetProducts();
        products.Remove(p2);

        // Assert
        Assert.AreEqual(products, result);
    }

    [TestMethod]
    public void GetProducts_Empty_OK()
    {
        // Arrange
        var products = new List<Product>();
        var mock = new Mock<IProductRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetProducts(It.IsAny<Func<Product, bool>>())).Returns(products);
        var logic = new ProductLogic(mock.Object);

        // Act
        var result = logic.GetProducts();

        // Assert
        Assert.AreEqual(products, result);
    }

    [TestMethod]
    public void AddProduct_OK()
    {
        // Arrange
        var product = new Product
        {
            Name = "Prod", Price = 400,
            Description = "Test Description",
            Category = new Category { Name = "Test" },
            Brand = new Brand { Name = "Test" },
            Colors = new List<Color> { new() { Name = "Test" } }
        };
        var mock = new Mock<IProductRepository>(MockBehavior.Strict);
        mock.Setup(x => x.AddProduct(product)).Returns(product);
        var logic = new ProductLogic(mock.Object);

        // Act
        var result = logic.AddProduct(product);

        // Assert
        Assert.AreEqual(product, result);
    }

    [TestMethod]
    public void DeleteProduct_OK()
    {
        var product = new Product
        {
            Name = "Prod",
            Price = 400,
            Description = "Test Description",
            Category = new Category { Name = "Test" },
            Brand = new Brand { Name = "Test" },
            Colors = new List<Color> { new() { Name = "Test" } }
        };
        var mock = new Mock<IProductRepository>(MockBehavior.Strict);
        mock.Setup(x => x.SoftDelete(It.IsAny<Guid>())).Returns(() =>
        {
            product.IsDeleted = true;
            return product;
        });

        var logic = new ProductLogic(mock.Object);

        // Act
        var result = logic.SoftDelete(product.Id);

        // Assert
        Assert.AreEqual(true, result.IsDeleted);
    }

    [TestMethod]
    public void GetProducts_Predicate_OK()
    {
        var predicate = (Product p) => true;
        var products = new List<Product>
        {
            new() { Name = "Test", Price = 420, Description = "Test Description" },
            new() { Name = "Test2", Price = 69, Description = "Test Description2" }
        };

        var mock = new Mock<IProductRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetProducts(predicate)).Returns(products);
        var logic = new ProductLogic(mock.Object);

        // Act
        var result = logic.GetProducts(predicate);

        // Assert
        Assert.AreEqual(products, result);
    }

    [TestMethod]
    public void IsPurchaseValid_Valid_OK()
    {
        var cart = new List<PurchaseProduct>
        {
            new() { ProductId = Guid.NewGuid(), Quantity = 1 },
        };
        var products = new List<Product>
        {
            new() { Id = cart[0].ProductId, Stock = 2 },
        };

        var mock = new Mock<IProductRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetProduct(It.IsAny<Guid>())).Returns(products[0]);
        var logic = new ProductLogic(mock.Object);

        // Act
        logic.IsPurchaseValid(cart);

        // Assert
        Assert.IsTrue(true);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IsPurchaseValid_Valid_FAIL()
    {
        var cart = new List<PurchaseProduct>
        {
            new() { ProductId = Guid.NewGuid(), Quantity = 5 },
        };
        var products = new List<Product>
        {
            new() { Id = cart[0].ProductId, Stock = 2 },
        };

        var mock = new Mock<IProductRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetProduct(It.IsAny<Guid>())).Returns(products[0]);
        var logic = new ProductLogic(mock.Object);

        // Act
        logic.IsPurchaseValid(cart);
    }

    [TestMethod]
    public void DecreaseStock_Valid_OK()
    {
        var product = new Product
        {
            Name = "Prod",
            Price = 400,
            Description = "Test Description",
            Category = new Category { Name = "Test" },
            Brand = new Brand { Name = "Test" },
            Colors = new List<Color> { new() { Name = "Test" } },
            Stock = 5
        };
        var mock = new Mock<IProductRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetProduct(It.IsAny<Guid>())).Returns(product);
        mock.Setup(x => x.UpdateProduct(It.IsAny<Guid>(), It.IsAny<Product>())).Returns(product);
        var logic = new ProductLogic(mock.Object);

        // Act
        var result = logic.DecreaseStock(product.Id, 2);

        // Assert
        Assert.AreEqual(3, result.Stock);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DecreaseStock_Valid_FAIL()
    {
        var product = new Product
        {
            Name = "Prod",
            Price = 400,
            Description = "Test Description",
            Category = new Category { Name = "Test" },
            Brand = new Brand { Name = "Test" },
            Colors = new List<Color> { new() { Name = "Test" } },
            Stock = 10
        };
        var mock = new Mock<IProductRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetProduct(It.IsAny<Guid>())).Returns(product);
        mock.Setup(x => x.UpdateProduct(It.IsAny<Guid>(), It.IsAny<Product>())).Returns(product);
        var logic = new ProductLogic(mock.Object);

        // Act
        var result = logic.DecreaseStock(product.Id, 20);
    }

    [TestMethod]
    public void UpdateProduct_OK()
    {
        var product = new Product
        {
            Name = "Prod",
            Price = 400,
            Description = "Test Description",
            Category = new Category { Name = "Test" },
            Brand = new Brand { Name = "Test" },
            Colors = new List<Color> { new() { Name = "Test" } },
            Stock = 10
        };
        var product2 = new Product
        {
            Name = "Prod2",
            Price = 500,
            Description = "Test Description2",
            Category = new Category { Name = "Test" },
            Brand = new Brand { Name = "Test" },
            Colors = new List<Color> { new() { Name = "Test" } },
            Stock = 10
        };
        var mock = new Mock<IProductRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetProduct(It.IsAny<Guid>())).Returns(product);
        mock.Setup(x => x.UpdateProduct(It.IsAny<Guid>(), It.IsAny<Product>())).Returns(product);
        var logic = new ProductLogic(mock.Object);

        //Act
        var result = logic.UpdateProduct(product.Id, product2);

        //Assert
        Assert.AreEqual(result.Id, product.Id);

    }
}