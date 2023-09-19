using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Tests;

[TestClass]
public class ProductTests
{
    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(dbName).Options;
        return new Context(options);
    }
    
    [TestMethod]
    public void AddProduct_CorrectProduct_OK()
    {
        // Arrange
        var context = CreateDbContext("AddProduct_CorrectProduct_OK");
        var productRepository = new ProductRepository(context);

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand(){Name="Gucci"},
            Category = new Category(){Name="Bag"},
            Colors = new List<Color>() {new(){Name="Red"}},
        };

        // Act
        var result = productRepository.AddProduct(product);

        // Assert
        Assert.AreEqual(product, result);
    }
    [TestMethod]
    public void AddProduct_AddProductTwice_Fail()
    {
        // Arrange
        var context = CreateDbContext("AddProduct_CorrectProduct_OK");
        var productRepository = new ProductRepository(context);

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand(){Name="Gucci"},
            Category = new Category(){Name="Bag"},
            Colors = new List<Color>() {new(){Name="Red"}},
        };

        // Act
        productRepository.AddProduct(product);

        // Assert
        Assert.ThrowsException<ArgumentException>(() => productRepository.AddProduct(product));
    }
    
    [TestMethod]
    public void GetProduct_CorrectId_OK()
    {
        // Arrange
        var context = CreateDbContext("GetProduct_CorrectId_OK");
        var productRepository = new ProductRepository(context);

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand(){Name="Gucci"},
            Category = new Category(){Name="Bag"},
            Colors = new List<Color>() {new(){Name="Red"}},
        };
        context.Set<Product>().Add(product);
        context.SaveChanges();

        // Act
        var result = productRepository.GetProduct(product.Id);

        // Assert
        Assert.AreEqual(product, result);
    }
    
    [TestMethod]
    public void GetProduct_IncorrectId_Null()
    {
        // Arrange
        var context = CreateDbContext("GetProduct_IncorrectId_Null");
        var productRepository = new ProductRepository(context);

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand(){Name="Gucci"},
            Category = new Category(){Name="Bag"},
            Colors = new List<Color>() {new(){Name="Red"}},
        };
        context.Set<Product>().Add(product);
        context.SaveChanges();

        // Act
        var result = productRepository.GetProduct(product.Id + 1);

        // Assert
        Assert.IsNull(result);
    }
    
    [TestMethod]
    public void GetProducts_ValidProducts_OK()
    {
        // Arrange
        var context = CreateDbContext("GetProducts_CorrectId_OK");
        var productRepository = new ProductRepository(context);

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand(){Name="Gucci"},
            Category = new Category(){Name="Bag"},
            Colors = new List<Color>() {new(){Name="Red"}},
        };
        context.Set<Product>().Add(product);
        context.SaveChanges();

        // Act
        var result = productRepository.GetProducts();

        // Assert
        Assert.AreEqual(product, result[0]);
    }
}