using DataAccess.Interfaces;
using Domain;
using Moq;

namespace Logic.Tests;

[TestClass]
public class PurchaseLogicTest
{
    [TestMethod]
    public void AddProductToCart_Valid_OK()
    {
        // Arrange
        var product = new Product { Name = "Test", Price = 420, Description = "Test Description"};
        var session = new SessionToken();
        var cart = new Purchase();
        
        var mock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
        mock.Setup(x => x.AddProducts(It.IsAny<Purchase>(), It.IsAny<List<Product>>())).Returns(() =>
        {
            cart.AddProduct(product);
            return cart;
        });
        var logic = new PurchaseLogic(mock.Object);
        
        // Act
       var result = logic.AddProduct(product, cart);
       
        // Assert
        Assert.AreEqual(1, result.Products.Count);
    }
    
    [TestMethod]
    public void DeleteProductFromCart_Valid_OK()
    {
        // Arrange
        var product = new Product { Name = "Test", Price = 420, Description = "Test Description"};
        var session = new SessionToken();
        var cart = new Purchase();
        
        var mock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
        mock.Setup(x => x.DeleteProduct(It.IsAny<Purchase>(), It.IsAny<Product>())).Returns(() =>
        {
            cart.DeleteProduct(product);
            return cart;
        });
        var logic = new PurchaseLogic(mock.Object);
        
        // Act
       var result = logic.DeleteProduct(product, cart);
       
        // Assert
        Assert.AreEqual(0, result.Products.Count);
    }
    [TestMethod]
    public void AddCart_Valid_OK()
    {
        // Arrange
        var session = new SessionToken();
        var cart = new Purchase();
        
        var mock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
        mock.Setup(x => x.AddPurchase(It.IsAny<Purchase>())).Returns(() =>
        {
            return cart;
        });
        var logic = new PurchaseLogic(mock.Object);
        
        // Act
       var result = logic.AddCart(cart);
       
        // Assert
        Assert.AreEqual(cart, result);
    }
}