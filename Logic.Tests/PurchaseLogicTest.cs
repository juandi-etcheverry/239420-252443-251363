using DataAccess.Interfaces;
using Domain;
using Logic.Interfaces;
using Moq;
using PromotionStrategies;

namespace Logic.Tests;

[TestClass]
public class PurchaseLogicTest
{
    [TestMethod]
    public void AddProductToCart_Valid_OK()
    {
        // Arrange
        var products = new List<Product>();
        var product = new Product { Name = "Test", Price = 420, Description = "Test Description"};
        products.Add(product);
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
       var result = logic.AddProducts(products, cart);
       
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
    
    [TestMethod]
    public void SetFinalPrice_Valid_OK()
    {
        // Arrange
        var products = new List<Product>();
        var product1 = new Product { Name = "Test1", Price = 420, Description = "Test Description"};
        var product2 = new Product { Name = "Test2", Price = 500, Description = "Test Description"};
        products.Add(product1);
        products.Add(product2);
        var cart = new Purchase();
        
        var mockPurchase = new Mock<IPurchaseRepository>(MockBehavior.Strict);
        var mockPromotion = new Mock<IPromotionLogic>(MockBehavior.Strict);
        var mockPromotionStrategy = new Mock<IPromotionStrategy>(MockBehavior.Strict);
        
        mockPurchase.Setup(x => x.AddProducts(It.IsAny<Purchase>(), It.IsAny<List<Product>>())).Returns(() =>
        {
            cart.AddProducts(products);
            return cart;
        });
        mockPromotion.Setup(x=>x.GetBestPromotion(It.IsAny<List<Product>>())).Returns(() =>
        {
            return new TwentyPercentPromotionStrategy();
        });
        var logic = new PurchaseLogic(mockPurchase.Object, mockPromotion.Object);
        
        // Act
       var result = logic.AddProducts(products, cart);
       logic.SetFinalPrice(result);
       
        // Assert
        Assert.AreEqual(820, result.FinalPrice);
    }
}