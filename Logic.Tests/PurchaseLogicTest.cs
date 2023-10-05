using DataAccess.Interfaces;
using Domain;
using Logic.Interfaces;
using Moq;
using PromotionStrategies;
using TypeHelper;

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
        var user = new User()
        {
            Email = "test@test.com",
            Address = "Cuareim 1234",
            Role = Role.Buyer,
            Password = "Password123"
        };
        Product newProduct = new Product { Name = "Test", Price = 420, Description = "Test Description" };

        cart.User = user;
        cart.Products = new List<Product>() { newProduct };

        var mockPurchaseRepository = new Mock<IPurchaseRepository>(MockBehavior.Strict);
        mockPurchaseRepository.Setup(x => x.AddPurchase(It.IsAny<Purchase>())).Returns(() =>
        {
            return cart;
        });

        var mockPromotionLogic = new Mock<IPromotionLogic>(MockBehavior.Strict);
        mockPromotionLogic.Setup(m => m.GetBestPromotion(It.IsAny<List<Product>>()))
            .Returns(new TwentyPercentPromotionStrategy());

        var logic = new PurchaseLogic(mockPurchaseRepository.Object, mockPromotionLogic.Object);
        
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

    [TestMethod]
    public void GetAllPurchasesHistoryOK()
    {
        //Arrange
        var user = new User();
        var purchases = new List<Purchase>();
        var purchase = new Purchase();
        purchases.Add(purchase);
        var mock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetAllPurchasesHistory(It.IsAny<User>())).Returns(() =>
        {
            return purchases;
        });

        var logic = new PurchaseLogic(mock.Object);
        
        //Act
        var result = logic.GetAllPurchasesHistory(user);
        
        //Assert
        Assert.AreEqual(purchases, result);
    }
    
    [TestMethod]
    public void GetAllPurchasesHistory_Empty_OK()
    {
        //Arrange
        var user = new User();
        var purchases = new List<Purchase>();
        var mock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetAllPurchasesHistory(It.IsAny<User>())).Returns(() =>
        {
            return purchases;
        });

        var logic = new PurchaseLogic(mock.Object);
        
        //Act
        var result = logic.GetAllPurchasesHistory(user);
        
        //Assert
        Assert.AreEqual(purchases, result);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "There are no purchases")]
    public void GetAllPurchasesHistory_AllPurchases_Fail()
    {
        //Arrange
        var mock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetAllPurchasesHistory()).Throws(() =>
        {
            throw new ArgumentException("There are no purchases");
        });

        var logic = new PurchaseLogic(mock.Object);

        //Act
        var result = logic.GetAllPurchasesHistory();
    }

    [TestMethod]
    public void GetAllPurchasesNoParams_OK()
    {
        //Arrange
        var purchases = new List<Purchase>();
        var purchase = new Purchase();
        purchases.Add(purchase);
        var mock = new Mock<IPurchaseRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetAllPurchasesHistory()).Returns(() =>
        {
            return purchases;
        });

        var logic = new PurchaseLogic(mock.Object);
        
        //Act
        var result = logic.GetAllPurchasesHistory();
        
        //Assert
        Assert.AreEqual(purchases, result);
    }
}