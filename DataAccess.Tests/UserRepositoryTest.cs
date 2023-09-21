using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Tests
{
    [TestClass]
    public class UserRepositoryTest
	{
        private DbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(dbName).Options;
            return new Context(options);
        }

        [TestMethod]
        public void AddUser_CorrectUser_OK()
        {
            //Arrange
            var context = CreateDbContext("AddUser_CorrectUser_OK");
            var userRepository = new UserRepository(context);

            var user = new User
            {
                Email = "test@gmail.com",
                Role = Role.Comprador,
                Address = "Mercedes 2331"
            };

            //Act
            var result = userRepository.AddUser(user);

            //Assert
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void AddUser_AddUserTwice_Fail()
        {
            //Arrgange
            var context = CreateDbContext("AddUser_CorrectUser_OK");
            var userRepository = new UserRepository(context);

            var user = new User
            {
                Email = "test@gmail.com",
                Role = Role.Comprador,
                Address = "Mercedes 2331"
            };

            //Act
            userRepository.AddUser(user);

            //Assert
            Assert.ThrowsException<ArgumentException>(() => userRepository.AddUser(user));
        }

        [TestMethod]
        public void GetUser_CorrectId_OK()
        {
            //Arrange
            var context = CreateDbContext("GetUser_CorrectId_OK");
            var userRepository = new UserRepository(context);

            var user = new User
            {
                Email = "test@gmail.com",
                Role = Role.Comprador,
                Address = "Mercedes 2331"
            };
            context.Set<User>().Add(user);
            context.SaveChanges();

            //Act
            var result = userRepository.GetUser(user.Id);

            //Assert
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User with id -1 not found")]
        public void GetUser_IncorrectId_Null()
        {
            //Arrange
            var context = CreateDbContext("GetUser_IncorrectId_Null");
            var userRepository = new UserRepository(context);

            var user = new User
            {
                Email = "test@gmail.com",
                Role = Role.Comprador,
                Address = "Mercedes 2331"
            };
            context.Set<User>().Add(user);
            context.SaveChanges();

            //Act
            userRepository.GetUser(-1);
        }

        [TestMethod]
        public void GetUser_SoftDeletedUsers_OK()
        {
            //Arrange
            var context = CreateDbContext("GetUser_SoftDeletedUsers_OK");
            var userRepository = new UserRepository(context);

            var user = new User
            {
                Email = "test@gmail.com",
                Role = Role.Comprador,
                Address = "Mercedes 2331",
                IsDeleted = true
            };
            context.Set<User>().Add(user);
            context.SaveChanges();

            //Act
            var result = userRepository.GetUser(user.Id);

            //Assert
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void SoftDelete_CorrectId_OK()
        {
            //Arrange
            var context = CreateDbContext("SoftDelete_CorrectId_OK");
            var userRepository = new UserRepository(context);

            var user = new User
            {
                Email = "test@gmail.com",
                Role = Role.Comprador,
                Address = "Mercedes 2331",
                IsDeleted = false
            };
            context.Set<User>().Add(user);
            context.SaveChanges();

            //Act
            var result = userRepository.SoftDelete(user.Id);

            //Assert
            Assert.AreEqual(true, result.IsDeleted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User with id -1 not found")]
        public void SoftDelete_IncorrectId_FAIL()
        {
            //Arrange
            var context = CreateDbContext("SoftDelete_IncorrectId_Null");
            var userRepository = new UserRepository(context);

            var user = new User
            {
                Email = "test@gmail.com",
                Role = Role.Comprador,
                Address = "Mercedes 2331",
                IsDeleted = false
            };
            context.Set<User>().Add(user);
            context.SaveChanges();

            //Act
            userRepository.SoftDelete(-1);
        }
    }
}

