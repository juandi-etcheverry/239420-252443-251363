using DataAccess.Interfaces;
using Domain;
using Moq;
using TypeHelper;

namespace Logic.Tests;

[TestClass]
public class SessionLogicTest
{
    [TestMethod]
    public void GetSession_ValidId_OK()
    {
        //Arrange
        var user = new User
        {
            Email = "test@gmail.com",
            Role = Role.Comprador,
            Address = "Cuareim 1234",
        };
        var session = new SessionToken
        {
            User = user
        };
        var mock = new Mock<ISessionRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetSessionToken(It.IsAny<Guid>())).Returns(session);
        var logic = new SessionTokenLogic(mock.Object);

        //Act
        var result = logic.GetSessionToken(session.Id);

        //Assert
        Assert.AreEqual(session, result);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Session  not found")]
    public void GetSession_InvalidId_FAIL()
    {
        //Arrange
        var user = new User
        {
            Email = "test@gmail.com",
            Role = Role.Comprador,
            Address = "Cuareim 1234",
        };
        var session = new SessionToken
        {
            User = user
        };
        var mock = new Mock<ISessionRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetSessionToken(It.IsAny<Guid>())).Throws(new ArgumentException("Session  not found"));
        var logic = new SessionTokenLogic(mock.Object);

        //Act
        var result = logic.GetSessionToken(session.Id);
    }

    [TestMethod]
    public void AddSession_ValidSession_OK()
    {
        //Arrange
        var user = new User
        {
            Email = "test@gmail.com",
            Role = Role.Comprador,
            Address = "Cuareim 1234",
        };
        var session = new SessionToken
        {
            User = user
        };
        var mock = new Mock<ISessionRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetSessionToken(It.IsAny<Guid>())).Returns(session);
        var logic = new SessionTokenLogic(mock.Object);
        
        //Act
        var result = logic.AddSessionToken(session);
        
        //Assert
        Assert.AreEqual(session, result);
    }
    
    [TestMethod]
    public void AddSession_NullUser_OK()
    {
        //Arrange
        var session = new SessionToken();
        var mock = new Mock<ISessionRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetSessionToken(It.IsAny<Guid>())).Returns(session);
        var logic = new SessionTokenLogic(mock.Object);
        
        //Act
        var result = logic.AddSessionToken(session);
        
        //Assert
        Assert.AreEqual(result, session);
    }


}