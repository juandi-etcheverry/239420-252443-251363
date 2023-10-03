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
}
