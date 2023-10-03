using System.Diagnostics.Contracts;
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
            Role = Role.Buyer,
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
            Role = Role.Buyer,
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
            Role = Role.Buyer,
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
    [TestMethod]
    public void AddSession_RepeatedSession_OK()
    {
        //Arrange
        var user = new User
        {
            Email = "test@gmail.com",
            Role = Role.Buyer,
            Address = "Cuareim 1234",
        };
        var session = new SessionToken
        {
            User = user
        };
        var mock = new Mock<ISessionRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetSessionToken(It.IsAny<Guid>())).Returns(session);
        var logic = new SessionTokenLogic(mock.Object);

        var result1 = logic.AddSessionToken(session);
    
        //Act
        var result2 = logic.AddSessionToken(session);
        
        //Assert
        Assert.AreEqual(result1, result2);
    }

    [TestMethod]
    public void DeleteSession_ValidSession_OK()
    {
        //Arrange
        var session = new SessionToken();
        var mock = new Mock<ISessionRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetSessionToken(It.IsAny<Guid>())).Returns(session);
        mock.Setup(x => x.AddSessionToken(It.IsAny<SessionToken>())).Returns(session);
        mock.Setup(x => x.DeleteSession(It.IsAny<Guid>()));
        var logic = new SessionTokenLogic(mock.Object);

        logic.AddSessionToken(session);
        
        //Act
        var result = logic.DeleteSessionToken(session);
        
        //Assert
        Assert.AreEqual(true, result);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Session  not found")]
    public void DeleteSession_InvalidSession_FAIL()
    {
        //Arrange
        var session = new SessionToken();
        var mock = new Mock<ISessionRepository>(MockBehavior.Strict);
        mock.Setup((x => x.DeleteSession(It.IsAny<Guid>()))).Throws(new ArgumentException("Session  not found"));
        var logic = new SessionTokenLogic(mock.Object);

        //Act
        var result = logic.DeleteSessionToken(session);
    }

    [TestMethod]
    public void AddSession_SessionNotFoundOK()
    {
        //Arrange
        var session = new SessionToken();
        var mock = new Mock<ISessionRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetSessionToken(It.IsAny<Guid>())).Throws(new ArgumentException("Session  not found"));
        mock.Setup(x => x.AddSessionToken(It.IsAny<SessionToken>())).Returns(session);
        var logic = new SessionTokenLogic(mock.Object);
        
        //Act
        var result = logic.AddSessionToken(session);
        
        //Assert
        Assert.AreEqual(session, result);
    }

    [TestMethod]
    public void SessionExists_ValidSession_OK()
    {
        //Arrange
        var session = new SessionToken();
        var mock = new Mock<ISessionRepository>(MockBehavior.Strict);
        mock.Setup(x => x.SessionTokenExists(It.IsAny<Guid>())).Returns(true);
        var logic = new SessionTokenLogic(mock.Object);
        
        //Act
        var result = logic.SessionTokenExists(session.Id);
        
        //Assert
        Assert.AreEqual(true, result);
    }

    [TestMethod]
    public void SessionExists_InvalidSession_OK()
    {
        //Arrange
        var session = new SessionToken();
        var mock = new Mock<ISessionRepository>(MockBehavior.Strict);
        mock.Setup(x => x.SessionTokenExists(It.IsAny<Guid>())).Returns(false);
        var logic = new SessionTokenLogic(mock.Object);
        
        //Act
        var result = logic.SessionTokenExists(session.Id);
        
        //Assert
        Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void AddUserToSession_ValidSession_OK()
    {
        //Arrange
        var user = new User
        {
            Email = "test@test.com",
            Address = "Cuareim 1234",
            Role = Role.Buyer,
            Password = "Password123"
        };

        var session = new SessionToken();
        var mock = new Mock<ISessionRepository>(MockBehavior.Strict);
        mock.Setup(x => x.GetSessionToken(It.IsAny<Guid>())).Returns(session);
        mock.Setup(x => x.UpdateUserSessionToken(It.IsAny<Guid>(), It.IsAny<User>())).Returns(() =>
        {
            session.User = user;
            return session;
        });

        var logic = new SessionTokenLogic(mock.Object);

        //Act
        var result = logic.AddUserToToken(new Guid(), user);

        //Assert
        Assert.AreEqual(session.User, result.User);
    }
}