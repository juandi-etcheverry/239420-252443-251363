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
        var product = new Product { Name = "Test", Price = 420, Description = "Test Description"};
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
        var product = new Product { Name = "Test", Price = 420, Description = "Test Description"};
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
        var product = new Product { Name = "Test", Price = 420, Description = "Test Description", IsDeleted = true};
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
            new Product { Name = "Test", Price = 420, Description = "Test Description"},
            new Product { Name = "Test2", Price = 69, Description = "Test Description2"}
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
        var p1 = new Product { Name = "Test", Price = 420, Description = "Test Description"};
        var p2 = new Product { Name = "Test2", Price = 69, Description = "Test Description2", IsDeleted = true};
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
            Colors = new List<Color> { new Color { Name = "Test" } }

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
            Colors = new List<Color> { new Color { Name = "Test" } }

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
}
