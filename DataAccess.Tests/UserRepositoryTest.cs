using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using TypeHelper;

namespace DataAccess.Tests
{
    [TestClass]
    public class UserRepositoryTest
    {

        private SessionToken _sessionToken;
        private User _user;

        private DbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(dbName).Options;
            var context =  new Context(options);

            _user = new User()
            {
                Email = "test@test.com",
                Password = "Password123",
                Role = Role.Buyer,
                Address = "Mercedes 2331"
            };
            context.Set<User>().Add(_user);


            _sessionToken = new SessionToken()
            {
                User = _user,
            };
            context.Set<SessionToken>().Add(_sessionToken);

            return context;
        }

        [TestMethod]
        public void AddUser_CorrectUser_OK()
        {
            //Arrange
            var context = CreateDbContext("AddUser_CorrectUser_OK");
            var userRepository = new UserRepository(context);


            //Act
            var result = userRepository.AddUser(_user);

            //Assert
            Assert.AreEqual(_user, result);
        }

        [TestMethod]
        public void AddUser_AddUserTwice_Fail()
        {
            //Arrgange
            var context = CreateDbContext("AddUser_CorrectUser_OK");
            var userRepository = new UserRepository(context);

            //Act
            userRepository.AddUser(_user);

            //Assert
            Assert.ThrowsException<ArgumentException>(() => userRepository.AddUser(_user));
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
                Password = "Password123",
                Role = Role.Buyer,
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
        [ExpectedException(typeof(ArgumentException), "User with random id not found")]
        public void GetUser_IncorrectId_Null()
        {
            //Arrange
            var context = CreateDbContext("GetUser_IncorrectId_Null");
            var userRepository = new UserRepository(context);

            var user = new User
            {
                Email = "test@gmail.com",
                Password = "Password123",
                Role = Role.Buyer,
                Address = "Mercedes 2331"
            };
            context.Set<User>().Add(user);
            context.SaveChanges();

            //Act
            userRepository.GetUser(Guid.NewGuid());
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
                Password = "Password123",
                Role = Role.Buyer,
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
                Password = "Password123",
                Role = Role.Buyer,
                Address = "Mercedes 2331",
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
                Password = "Password123",
                Role = Role.Buyer,
                Address = "Mercedes 2331",
            };
            context.Set<User>().Add(user);
            context.SaveChanges();

            //Act
            userRepository.SoftDelete(Guid.NewGuid());
        }

        [TestMethod]
        public void UpdateUser_CorrectUser_OK()
        {
            //Arrange
            var context = CreateDbContext("UpdateUser_CorrectUser_OK");
            var userRepository = new UserRepository(context);

            userRepository.AddUser(_user);

            var result = userRepository.UpdateUser(_user.Id,
                new User() { Address = "new Address", Email = "newEmail@gmail.com", Role = Role.Total });

            Assert.AreEqual("new Address", result.Address);
            Assert.AreEqual("newEmail@gmail.com", result.Email);
            Assert.AreEqual(Role.Total, result.Role);
        }

        [TestMethod]
        public void FindUser_OK_Test()
        {
            var context = CreateDbContext("FindUser_OK_Test");
            var userRepository = new UserRepository(context);

            userRepository.AddUser(_user);

            var result = userRepository.FindUser(_user.Email);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetUser_EmailAndPassword_OK()
        {
            //Arrange
            var context = CreateDbContext("GetUser_EmailAndPassword_OK");
            var userRepository = new UserRepository(context);

            userRepository.AddUser(_user);

            //Act
            var result = userRepository.GetUser(_user.Email, _user.Password);

            //Assert
            Assert.AreEqual(_user.Id, result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void GetUser_EmailAndPassword_FAIL()
        {
            //Arrange
            var context = CreateDbContext("GetUser_EmailAndPassword_FAIL");
            var userRepository = new UserRepository(context);

            userRepository.AddUser(_user);

            //Act
            var result = userRepository.GetUser("notTheEmail@test.com", "notThePassword");

            //Assert
            Assert.AreEqual(_user, result);
        }

        [TestMethod]
        public void FindUser_NotFound_OK()
        {
            var context = CreateDbContext("FindUser_NotFound_OK_Test");
            var userRepository = new UserRepository(context);

            var user = new User
            {
                Email = "test@gmail.com",
                Password = "Password123",
                Role = Role.Buyer,
                Address = "Mercedes 2331",
            };

            var result = userRepository.FindUser(user.Email);

            Assert.IsFalse(result);
        }
    }
}

