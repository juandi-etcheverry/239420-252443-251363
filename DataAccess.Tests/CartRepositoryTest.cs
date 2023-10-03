using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Tests;

[TestClass]
public class CartRepositoryTest
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
        var cartRepository = new CartRepository(context);
        var session = new SessionToken();
        var cart = new Cart
        {
            Session = session
        };
        //Act
        var result = cartRepository.AddCart(cart);
        
        //Assert
        Assert.AreEqual(cart, result);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Session is null")]
    public void AddCart_CartSessionNullFalse()
    {
        //Arrange
        var context = CreateDbContext("AddCart_CartSessionNullFalse");
        var cartRepository = new CartRepository(context);
        SessionToken session = null;
        var cart = new Cart
        {
            Session = session
        };
        //Act
        var result = cartRepository.AddCart(cart);
    }

    [TestMethod]
    public void AddProduct_CorrectProductOK()
    {
        //Arrange
        var context = CreateDbContext("AddProduct_CorrectProductOK");
        var cartRepository = new CartRepository(context);
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
        var session = new SessionToken();
        var cart = new Cart { Session = session};
        var cartResult = cartRepository.AddCart(cart);
        
        //Act
        var result = cartRepository.AddProduct(cartResult, product);
        
        //Assert
        Assert.AreEqual(result.Products[0].Id, product.Id);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Product is null")]
    public void AddProduct_NullProductFail()
    {
        //Arrange
        var context = CreateDbContext("AddProduct_CorrectProductOK");
        var cartRepository = new CartRepository(context);
        var session = new SessionToken();
        var cart = new Cart { Session = session};
        var cartResult = cartRepository.AddCart(cart);
        Product product = null;
        
        //Act
        var result = cartRepository.AddProduct(cartResult, product);
    }
    
    [TestMethod]
    public void DeleteProduct_CorrectProductOK()
    {
        //Arrange
        var context = CreateDbContext("DeleteProduct_CorrectProductOK");
        var cartRepository = new CartRepository(context);
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
        var session = new SessionToken();
        var cart = new Cart { Session = session};
        var cartResult = cartRepository.AddCart(cart);
        var cartResultWithProduct = cartRepository.AddProduct(cartResult, product);
        
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
        var cartRepository = new CartRepository(context);
        var session = new SessionToken();
        var cart = new Cart { Session = session};
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
        var cartRepository = new CartRepository(context);
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
        Cart cart = null;
        
        //Act
        var result = cartRepository.AddProduct(cart, product);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Cart is null")]
    public void DeleteProduct_NullCartFail()
    {
        //Arange
        var context = CreateDbContext("DeleteProduct_NullCartFail");
        var cartRepository = new CartRepository(context);
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
        Cart cart = null;
        
        //Act
        var result = cartRepository.DeleteProduct(cart, product);
    }
}
