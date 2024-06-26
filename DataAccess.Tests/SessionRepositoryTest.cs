﻿using Domain;
using Microsoft.EntityFrameworkCore;
using TypeHelper;

namespace DataAccess.Tests;

[TestClass]
public class SessionRepositoryTest
{
    private Purchase _cart;
    private SessionToken _session;

    private User _user;

    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(dbName).Options;

        var context = new Context(options);


        _user = new User
        {
            Email = "test@test.com",
            Password = "Password123",
            Role = Role.Buyer,
            Address = "Cuareim 1234"
        };
        context.Set<User>().Add(_user);

        _cart = new Purchase { User = _user };
        context.Set<Purchase>().Add(_cart);

        _session = new SessionToken { User = _user, Cart = _cart };

        return context;
    }

    [TestMethod]
    public void AddSession_CorrectSession_OK()
    {
        //Arrgange
        var context = CreateDbContext("AddSession_CorrectSession_OK");
        var sessionRepository = new SessionRepository(context);
        var userRepository = new UserRepository(context);

        var userResult = userRepository.AddUser(_user);


        //Act
        var sessionResult = sessionRepository.AddSessionToken(_session);

        //Assert
        Assert.AreEqual(sessionResult.User.Id, userResult.Id);
    }

    [TestMethod]
    public void AddSession_AddTwoSessionsSameUser_OK()
    {
        //Arrange
        var context = CreateDbContext("AddSession_AddTwoSessionsSameUser_OK");
        var sessionRepository = new SessionRepository(context);
        var userRepository = new UserRepository(context);

        var user = new User
        {
            Email = "test@gmail.com",
            Password = "Password123",
            Role = Role.Buyer,
            Address = "Cuareim 1234"
        };
        var userResult = userRepository.AddUser(user);

        var session1 = new SessionToken
        {
            User = userResult
        };
        var sessionResult1 = sessionRepository.AddSessionToken(session1);

        var session2 = new SessionToken
        {
            User = userResult
        };

        //Act
        var sessionResult2 = sessionRepository.AddSessionToken(session2);

        //Assert
        Assert.AreNotEqual(sessionResult1.Id, sessionResult2.Id);
    }

    [TestMethod]
    public void AddSession_AddTwoSessionsDifferentUser_OK()
    {
        //Arrange
        var context = CreateDbContext("AddSession_AddTwoSessionsDifferentUser_OK");
        var sessionRepository = new SessionRepository(context);
        var userRepository = new UserRepository(context);

        var user1 = new User
        {
            Email = "test1@gmail.com",
            Password = "Password123",
            Role = Role.Buyer,
            Address = "Cuareim 1234"
        };
        var userResult1 = userRepository.AddUser(user1);

        var user2 = new User
        {
            Email = "test2@gmail.com",
            Password = "Password123",
            Role = Role.Buyer,
            Address = "Cuareim 1234"
        };
        var userResult2 = userRepository.AddUser(user2);

        var session1 = new SessionToken
        {
            User = userResult1
        };
        var sessionResult1 = sessionRepository.AddSessionToken(session1);

        //Act
        var session2 = new SessionToken
        {
            User = userResult2
        };
        var sessionResult2 = sessionRepository.AddSessionToken(session2);

        //Assert
        Assert.AreNotEqual(sessionResult1.Id, sessionResult2.Id);
    }

    [TestMethod]
    public void GetSession_CorrectId_OK()
    {
        //Arrange
        var context = CreateDbContext("GetSession_CorrectId");
        var sessionRepository = new SessionRepository(context);
        var user = new User
        {
            Email = "test@gmail.com",
            Password = "Password123",
            Role = Role.Buyer,
            Address = "Cuareim 1234"
        };
        context.Set<User>().Add(user);
        context.SaveChanges();

        var session = new SessionToken
        {
            User = user
        };
        context.Set<SessionToken>().Add(session);
        context.SaveChanges();

        //Act
        var result = sessionRepository.GetSessionToken(session.Id);

        //Assert
        Assert.AreEqual(session, result);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Session not found")]
    public void GetSession_IncorrectId_Null()
    {
        //Arrange
        var context = CreateDbContext("GetSession_IncorrectId_Null");
        var sessionRepository = new SessionRepository(context);
        var user = new User
        {
            Email = "test@gmail.com",
            Password = "Password123",
            Role = Role.Buyer,
            Address = "Cuareim 1234"
        };
        context.Set<User>().Add(user);
        context.SaveChanges();

        var session = new SessionToken
        {
            User = user
        };
        context.Set<SessionToken>().Add(session);
        context.SaveChanges();
        //Act
        sessionRepository.GetSessionToken(Guid.NewGuid());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Session not found")]
    public void GetSession_Deleted_Null()
    {
        //Arrange
        var context = CreateDbContext("GetSession_Deleted_Null");
        var sessionRepository = new SessionRepository(context);

        var user = new User
        {
            Email = "test@gmail.com",
            Password = "Password123",
            Role = Role.Buyer,
            Address = "Cuareim 1234"
        };
        context.Set<User>().Add(user);
        context.SaveChanges();

        var session = new SessionToken
        {
            User = user
        };
        context.Set<SessionToken>().Add(session);
        context.SaveChanges();

        //Act
        sessionRepository.DeleteSession(session.Id);
        sessionRepository.GetSessionToken(session.Id);
    }

    [TestMethod]
    public void AddSession_InvalidUser_FAIL()
    {
        //Arrange
        var context = CreateDbContext("AddSession_InvalidUser_FAIL");
        var sessionRepository = new SessionRepository(context);
        var session = new SessionToken
        {
            User = null
        };
        //Act
        var sessionResult = sessionRepository.AddSessionToken(session);

        //Assert
        Assert.AreEqual(null, sessionResult.User);
    }

    [TestMethod]
    public void SessionExists_OK()
    {
        var context = CreateDbContext("SessionExists_OK");
        var sessionRepository = new SessionRepository(context);

        var token = sessionRepository.AddSessionToken(new SessionToken
        {
            User = new User
            {
                Email = "aaa@test.com",
                Address = "Cuareim 1234",
                Password = "Password123",
                Role = Role.Buyer
            }
        });

        var exists = sessionRepository.SessionTokenExists(token.Id);

        Assert.IsTrue(exists);
    }

    [TestMethod]
    public void UpdateUserSessionToken_OK()
    {
        var context = CreateDbContext("UpdateUserSessionToken_OK");
        var sessionRepository = new SessionRepository(context);
        var userRepository = new UserRepository(context);

        var user = new User
        {
            Email = "testing@test.com",
            Address = "Dot Net 1234",
            Password = "Password123",
            Role = Role.Buyer
        };

        var userResult = userRepository.AddUser(user);

        var session = new SessionToken();

        var sessionResult = sessionRepository.AddSessionToken(session);

        var updatedSession = sessionRepository.UpdateUserSessionToken(sessionResult.Id, userResult);

        Assert.AreEqual(userResult, updatedSession.User);
    }
}