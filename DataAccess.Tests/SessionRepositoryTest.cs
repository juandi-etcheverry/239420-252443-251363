using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using TypeHelper;

namespace DataAccess.Tests
{
    [TestClass]
    public class SessionRepositoryTest
    {
        private DbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(dbName).Options;
            return new Context(options);
        }

        [TestMethod]
        public void AddSession_CorrectSession_OK()
        {
            //Arrgange
            var context = CreateDbContext("AddSession_CorrectSession_OK");
            var sessionRepository = new SessionRepository(context);
            var userRepository = new UserRepository(context);

            var user = new User
            {
                Email = "test@gmail.com",
                Role = Role.Comprador,
                Address = "Cuareim 1234",
            };
            var userResult = userRepository.AddUser(user);

            var session = new SessionToken
            {
                User = userResult
            };

            //Act
            var sessionResult = sessionRepository.AddSessionToken(session);

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
                Role = Role.Comprador,
                Address = "Cuareim 1234",
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
                Role = Role.Comprador,
                Address = "Cuareim 1234",
            };
            var userResult1 = userRepository.AddUser(user1);

            var user2 = new User
            {
                Email = "test2@gmail.com",
                Role = Role.Comprador,
                Address = "Cuareim 1234",
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
                Role = Role.Comprador,
                Address = "Cuareim 1234",
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
        [ExpectedException(typeof(ArgumentException), $"Session not found")]
        public void GetSession_IncorrectId_Null()
        {
            //Arrange
            var context = CreateDbContext("GetSession_IncorrectId_Null");
            var sessionRepository = new SessionRepository(context);
            var user = new User
            {
                Email = "test@gmail.com",
                Role = Role.Comprador,
                Address = "Cuareim 1234",
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
        [ExpectedException(typeof(ArgumentException), $"Session not found")]
        public void GetSession_Deleted_Null()
        {
            //Arrange
            var context = CreateDbContext("GetSession_Deleted_Null");
            var sessionRepository = new SessionRepository(context);
            
            var user = new User
            {
                Email = "test@gmail.com",
                Role = Role.Comprador,
                Address = "Cuareim 1234",
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
        [ExpectedException(typeof(ArgumentException), $"Session must have a user")]
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
        }
    }
}