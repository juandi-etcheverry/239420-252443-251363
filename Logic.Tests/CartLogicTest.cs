using DataAccess.Interfaces;
using Domain;
using Moq;

namespace Logic.Tests;

[TestClass]
public class CartLogicTest
{
    [TestMethod]
    public void AddProductToCart_Valid_OK()
    {
        // Arrange
        var product = new Product { Name = "Test", Price = 420, Description = "Test Description"};
        var session = new SessionToken();
        var cart = new Cart { Session = session};
        
        var mock = new Mock<ICartRepository>(MockBehavior.Strict);
        mock.Setup(x => x.AddProduct(It.IsAny<Cart>(), It.IsAny<Product>())).Returns(() =>
        {
            cart.AddProduct(product);
            return cart;
        });
        var logic = new CartLogic(mock.Object);
        
        // Act
       var result = logic.AddProduct(product, cart);
       
        // Assert
        Assert.AreEqual(1, result.Products.Count);
       
    }
}