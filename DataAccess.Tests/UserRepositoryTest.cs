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
    }
}

