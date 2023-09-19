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
        var mock = new Mock<IProductRepository>();
        mock.Setup(x => x.GetProduct(product.Id)).Returns(product);
        var logic = new ProductLogic(mock.Object);
        
        // Act
        var result = logic.GetProduct(product.Id);
        
        // Assert
        Assert.AreEqual(product, result);
    }
}