using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Tests;

[TestClass]
public class Test
{
    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(dbName).Options;
        return new Context(options);
    }

    [TestMethod]
    public void AddCart_CorrectCart_OK()
    {
        //Arrange
        var context = CreateDbContext("AddCart_CorrectCart");
        var cartRepository = new PurchaseRepository(context);
        var session = new SessionToken();
        var cart = new Purchase();
        //Act
        var result = cartRepository.AddCart(cart);
        
        //Assert
        Assert.AreEqual(cart, result);
    }

    [TestMethod]
    public void AddProduct_CorrectProductOK()
    {
        //Arrange
        var context = CreateDbContext("AddProduct_CorrectProductOK");
        var cartRepository = new PurchaseRepository(context);
        var products = new List<Product>();
        var product = new Product{
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand(){Name="Gucci"},
            Category = new Category(){Name="Bag"},
            Colors = new List<Color>() {new(){Name="Red"}},
        };
        products.Add(product);
        context.Set<Product>().Add(product);
        context.SaveChanges();
        var session = new SessionToken();
        var cart = new Purchase();
        var cartResult = cartRepository.AddCart(cart);
        
        //Act
        var result = cartRepository.AddProducts(cartResult, products);
        
        //Assert
        Assert.AreEqual(result.Products[0].Id, product.Id);
    }

    [TestMethod]
    public void AddTwoProducts_CorrectProduct()
    {
        //Arrange
        var context = CreateDbContext("AddTwoProducts_CorrectProduct");
        var cartRepository = new PurchaseRepository(context);
        var products = new List<Product>();
        var product1 = new Product{
            Name = "Test Product1",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand(){Name="Gucci"},
            Category = new Category(){Name="Bag"},
            Colors = new List<Color>() {new(){Name="Red"}},
        };
        products.Add(product1);
        context.Set<Product>().Add(product1);
        context.SaveChanges();
        
        var product2 = new Product{
            Name = "Test Product1",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand(){Name="Gucci"},
            Category = new Category(){Name="Bag"},
            Colors = new List<Color>() {new(){Name="Red"}},
        };
        products.Add(product2);
        context.Set<Product>().Add(product2);
        context.SaveChanges();
        var session = new SessionToken();
        var cart = new Purchase();
        var cartResult = cartRepository.AddCart(cart);
        
        //Act
        var result = cartRepository.AddProducts(cartResult, products);
        
        //Assert
        Assert.AreEqual(result.Products.Count, 2);
    }

    [TestMethod]
    public void AddTwoProductsEqual_OK()
    {
        //Arrange
        var context = CreateDbContext("AddTwoProducts_CorrectProduct");
        var cartRepository = new PurchaseRepository(context);
        var products = new List<Product>();
        var product1 = new Product{
            Name = "Test Product1",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand(){Name="Gucci"},
            Category = new Category(){Name="Bag"},
            Colors = new List<Color>() {new(){Name="Red"}},
        };
        products.Add(product1);
        context.Set<Product>().Add(product1);
        context.SaveChanges();
        var session = new SessionToken();
        var cart = new Purchase();
        var cartResult = cartRepository.AddCart(cart);
        
        //Act
        products.Add(product1);
        var result = cartRepository.AddProducts(cartResult, products);
        
        //Assert
        Assert.AreEqual(result.Products.Count, 1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Product is null")]
    public void AddProduct_NullProductFail()
    {
        //Arrange
        var context = CreateDbContext("AddProduct_CorrectProductOK");
        var cartRepository = new PurchaseRepository(context);
        var session = new SessionToken();
        var cart = new Purchase();
        var cartResult = cartRepository.AddCart(cart);
        var products = new List<Product>();
        Product product = null;
        products.Add(product);
        
        //Act
        var result = cartRepository.AddProducts(cartResult, products);
    }
    
    [TestMethod]
    public void DeleteProduct_CorrectProductOK()
    {
        //Arrange
        var context = CreateDbContext("DeleteProduct_CorrectProductOK");
        var cartRepository = new PurchaseRepository(context);
        var products = new List<Product>();
        var product = new Product{
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand(){Name="Gucci"},
            Category = new Category(){Name="Bag"},
            Colors = new List<Color>() {new(){Name="Red"}},
        };
        products.Add(product);
        context.Set<Product>().Add(product);
        context.SaveChanges();
        var session = new SessionToken();
        var cart = new Purchase {};
        var cartResult = cartRepository.AddCart(cart);
        var cartResultWithProduct = cartRepository.AddProducts(cartResult, products);
        
        //Act
        var result = cartRepository.DeleteProduct(cartResultWithProduct, product);
        
        //Assert
        Assert.AreEqual(result.Products.Count, 0);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Product is null")]
    public void DeleteProduct_NullProductFail()
    {
        //Arrange
        var context = CreateDbContext("DeleteProduct_NullProductFail");
        var cartRepository = new PurchaseRepository(context);
        var session = new SessionToken();
        var cart = new Purchase {};
        var cartResult = cartRepository.AddCart(cart);
        Product product = null;
        
        //Act
        var result = cartRepository.DeleteProduct(cartResult, product);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Cart is null")]
    public void AddProduct_NullCartFail()
    {
        //Arrange
        var context = CreateDbContext("AddProduct_NullCartFail");
        var cartRepository = new PurchaseRepository(context);
        var products = new List<Product>();
        var product = new Product{
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand(){Name="Gucci"},
            Category = new Category(){Name="Bag"},
            Colors = new List<Color>() {new(){Name="Red"}},
        };
        products.Add(product);
        context.Set<Product>().Add(product);
        context.SaveChanges();
        Purchase purchase = null;
        
        //Act
        var result = cartRepository.AddProducts(purchase, products);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Cart is null")]
    public void DeleteProduct_NullCartFail()
    {
        //Arange
        var context = CreateDbContext("DeleteProduct_NullCartFail");
        var cartRepository = new PurchaseRepository(context);
        var product = new Product{
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand(){Name="Gucci"},
            Category = new Category(){Name="Bag"},
            Colors = new List<Color>() {new(){Name="Red"}},
        };
        context.Set<Product>().Add(product);
        context.SaveChanges();
        Purchase purchase = null;
        
        //Act
        var result = cartRepository.DeleteProduct(purchase, product);
    }

    [TestMethod]
    public void AssignUserToPurchase_OK()
    {
        var context = CreateDbContext("DeleteProduct_NullCartFail");
        var purchaseRepository = new PurchaseRepository(context);
        var user = new User();
        var purchase = new Purchase();
        var purchaseResult = purchaseRepository.AddCart(purchase);
        
        //Act
        purchaseRepository.AssignUserToPurchase(purchaseResult, user);
        
        //Assert
        Assert.AreEqual(purchaseResult.User, user);
    }
}
