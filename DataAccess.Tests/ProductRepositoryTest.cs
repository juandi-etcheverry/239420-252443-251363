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
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };

        // Act
        var result = productRepository.AddProduct(product);

        // Assert
        Assert.AreEqual(product, result);
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
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        context.Set<Product>().Add(product);
        context.SaveChanges();

        // Act
        var result = productRepository.GetProduct(product.Id);

        // Assert
        Assert.AreEqual(product, result);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Product with id -1 not found")]
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
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        context.Set<Product>().Add(product);
        context.SaveChanges();

        // Act
        productRepository.GetProduct(new Guid());
    }

    [TestMethod]
    public void GetProducts_ValidProducts_OK()
    {
        // Arrange
        var context = CreateDbContext("GetProducts_ValidProducts_OK");
        var productRepository = new ProductRepository(context);

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        context.Set<Product>().Add(product);
        context.SaveChanges();

        // Act
        var result = productRepository.GetProducts();

        // Assert
        Assert.AreEqual(product.Id, result[0].Id);
    }

    [TestMethod]
    public void GetProducts_NoProducts_OK()
    {
        // Arrange
        var context = CreateDbContext("GetProducts_NoProducts_Ok");
        var productRepository = new ProductRepository(context);

        // Act
        var result = productRepository.GetProducts();

        // Assert
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void GetProducts_SoftDeletedProducts_OK()
    {
        // Arrange
        var context = CreateDbContext("GetProducts_SoftDeletedProducts_OK");
        var productRepository = new ProductRepository(context);

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } },
            IsDeleted = true
        };
        context.Set<Product>().Add(product);
        context.SaveChanges();

        // Act
        var result = productRepository.GetProducts();

        // Assert
        Assert.AreEqual(product.Id, result[0].Id);
    }

    [TestMethod]
    public void GetProducts_TrivialTruePredicate_OK()
    {
        // Arrange
        var context = CreateDbContext("GetProducts_TrivialTruePredicate_OK");
        var productRepository = new ProductRepository(context);

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        context.Set<Product>().Add(product);
        context.SaveChanges();

        // Act
        var result = productRepository.GetProducts(p => true);

        // Assert
        Assert.AreEqual(product.Id, result[0].Id);
    }

    [TestMethod]
    public void GetProducts_TrivialFalsePredicate_OK()
    {
        // Arrange
        var context = CreateDbContext("GetProducts_TrivialFalsePredicate_OK");
        var productRepository = new ProductRepository(context);

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        context.Set<Product>().Add(product);
        context.SaveChanges();

        // Act
        var result = productRepository.GetProducts(p => false);

        // Assert
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void GetProducts_ProductsMatchingPredicate_OK()
    {
        // Arrange
        var context = CreateDbContext("GetProducts_ProductsMatchingPredicate_OK");
        var productRepository = new ProductRepository(context);

        var product1 = new Product
        {
            Name = "Test Product 1",
            Description = "Test Description 1",
            Price = 100,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        var product2 = new Product
        {
            Name = "Test Product 2",
            Description = "Test Description 2",
            Price = 100,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        context.Set<Product>().Add(product1);
        context.Set<Product>().Add(product2);
        context.SaveChanges();

        // Act
        var result = productRepository.GetProducts(p => p.Name.Contains("1"));

        // Assert
        Assert.AreEqual(product1.Id, result[0].Id);
    }

    [TestMethod]
    public void GetProducts_ProductsNotMatchingPredicate_OK()
    {
        // Arrange
        var context = CreateDbContext("GetProducts_ProductsNotMatchingPredicate_OK");
        var productRepository = new ProductRepository(context);

        var product1 = new Product
        {
            Name = "Test Product 1",
            Description = "Test Description 1",
            Price = 100,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        var product2 = new Product
        {
            Name = "Test Product 2",
            Description = "Test Description 2",
            Price = 100,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        context.Set<Product>().Add(product1);
        context.Set<Product>().Add(product2);
        context.SaveChanges();

        // Act
        var result = productRepository.GetProducts(p => p.Name.Contains("3"));

        // Assert
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void GetProducts_SoftDeletedPredicate_OK()
    {
        // Arrange
        var context = CreateDbContext("GetProducts_SoftDeletedPredicate_OK");
        var productRepository = new ProductRepository(context);

        var product1 = new Product
        {
            Name = "Test Product 1",
            Description = "Test Description 1",
            Price = 100,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } },
            IsDeleted = true
        };
        var product2 = new Product
        {
            Name = "Test Product 2",
            Description = "Test Description 2",
            Price = 100,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        context.Set<Product>().Add(product1);
        context.Set<Product>().Add(product2);
        context.SaveChanges();

        // Act
        var result = productRepository.GetProducts(p => p.IsDeleted);

        // Assert
        Assert.AreEqual(product1, result[0]);
    }

    [TestMethod]
    public void SoftDelete_CorrectId_OK()
    {
        // Arrange
        var context = CreateDbContext("SoftDelete_CorrectId_OK");
        var productRepository = new ProductRepository(context);

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            IsDeleted = false,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        context.Set<Product>().Add(product);
        context.SaveChanges();

        // Act
        var result = productRepository.SoftDelete(product.Id);

        // Assert
        Assert.AreEqual(true, result.IsDeleted);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Product with id -1 not found")]
    public void SoftDelete_IncorrectId_FAIL()
    {
        // Arrange
        var context = CreateDbContext("SoftDelete_IncorrectId_Null");
        var productRepository = new ProductRepository(context);

        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            IsDeleted = false,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        context.Set<Product>().Add(product);
        context.SaveChanges();

        // Act
        productRepository.SoftDelete(new Guid());
    }

    [TestMethod]
    public void UpdateProduct_CorrectProduct_OK()
    {
        // Arrange
        var context = CreateDbContext("UpdateProduct_CorrectProduct_OK");
        var productRepository = new ProductRepository(context);

        var product = new Product
        {
            Name = "Test Product 1",
            Description = "Test Description 1",
            Price = 100,
            IsDeleted = false,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color>
            {
                new() { Name = "Red" },
                new() { Name = "Blue" }
            },
            Stock = 10
        };
        context.Set<Product>().Add(product);
        context.SaveChanges();

        var updatedProduct = new Product
        {
            Name = "Test Product 2",
            Description = "Test Description 2",
            Price = 200,
            IsDeleted = false,
            Brand = product.Brand,
            Category = product.Category,
            Colors = new List<Color>
            {
                new() { Name = "Red" },
                new() { Name = "Blue" },
                new() { Name = "Green" }
            },
            Stock = 200
        };

        // Act
        var result = productRepository.UpdateProduct(product.Id, updatedProduct);

        // Assert
        Assert.AreEqual(updatedProduct.Stock, result.Stock);
    }   
}