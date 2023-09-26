﻿using System;
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
        
        
        
        
    }
}