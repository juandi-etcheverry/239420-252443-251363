using System;
using DataAccess.Interfaces;
using Domain;
using Moq;
using TypeHelper;

namespace Logic.Tests
{
	[TestClass]
	public class UserLogicTest
	{
		[TestMethod]
		public void GetUser_ValidId_OK()
		{
			//Arrange
			var user = new User
			{
				Email = "userTest@gmail.com",
				Password = "Password123",
				Role = Role.Comprador,
				Address = "Ejido 1234"
			};
			var mock = new Mock<IUserRepository>(MockBehavior.Strict);
			mock.Setup(x => x.GetUser(It.IsAny<Guid>())).Returns(user);
			var logic = new UserLogic(mock.Object);

			//Act
			var result = logic.GetUser(user.Id);

			//Assert
			Assert.AreEqual(user, result);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "User with id 0 not found")]
		public void GetUser_Invalid_FAIL()
		{
            //Arrange
            var user = new User
            {
                Email = "userTest@gmail.com",
                Password = "Password123",
                Role = Role.Comprador,
                Address = "Ejido 1234"
            };
            var mock = new Mock<IUserRepository>(MockBehavior.Strict);
            mock.Setup(x => x.GetUser(It.IsAny<Guid>())).Throws(new ArgumentException("User with id 0 not found"));
			var logic = new UserLogic(mock.Object);

			//Act
			var result = logic.GetUser(user.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User with id 1 not found")]
		public void GetUser_SoftDeleted_FAIL()
		{
			//Arrange
			var user = new User
			{
				Email = "userTest@gmail.com",
				Password = "Password123",
				Role = Role.Comprador,
				Address = "Ejido 1234",
				IsDeleted = true
            };
            var mock = new Mock<IUserRepository>(MockBehavior.Strict);
			mock.Setup(x => x.GetUser(It.IsAny<Guid>())).Returns(user);
			var logic = new UserLogic(mock.Object);

			//Act
			var result = logic.GetUser(user.Id);
        }


        [TestMethod]
        public void DeleteUser_Valid_OK()
        {
            var user = new User
            {
                Email = "userTest@gmail.com",
                Password = "Password123",
                Role = Role.Comprador,
                Address = "Ejido 1234",
            };

            var mock = new Mock<IUserRepository>(MockBehavior.Strict);
            mock.Setup(x => x.GetUser(It.IsAny<Guid>())).Returns(user);
            mock.Setup(x => x.SoftDelete(It.IsAny<Guid>())).Returns(() =>
            {
                user.IsDeleted = true;
                return user;
            });

			var logic = new UserLogic(mock.Object);

            var result = logic.DeleteUser(user.Id);

            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void GetUser_ValidEmailAndPassword_OK()
        {
            var user = new User
            {
                Email = "userTest@gmail.com",
                Password = "Password123",
                Role = Role.Comprador,
                Address = "Ejido 1234",
            };

            var mock = new Mock<IUserRepository>(MockBehavior.Strict);
            mock.Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>())).Returns(user);
            var logic = new UserLogic(mock.Object);

            var result = logic.GetUser("anyEmail@test.com", "anyPassword");

            Assert.AreEqual(user, result);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUser_IncorrectEmailAndPassword_FAIL()
        {
            var user = new User
            {
                Email = "userTest@gmail.com",
                Password = "Password123",
                Role = Role.Comprador,
                Address = "Ejido 1234",
            };

            var mock = new Mock<IUserRepository>(MockBehavior.Strict);
            mock.Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new ArgumentException());
            
            var logic = new UserLogic(mock.Object);

            var result = logic.GetUser("anyEmail@test.com", "anyPassword");
        }

        [TestMethod]
        public void CreateUser_Valid_OK()
        {
	        //Arrange
	        var user = new User
	        {
		        Email = "userTest@gmail.com",
		        Password = "Password123",
		        Role = Role.Comprador,
		        Address = "Ejido 1234",
	        };

	        var mock = new Mock<IUserRepository>(MockBehavior.Strict);
	        mock.Setup(u => u.AddUser(It.IsAny<User>())).Returns(user);
	        mock.Setup(u => u.FindUser(It.IsAny<string>())).Returns(false);

	        var logic = new UserLogic(mock.Object);
	        
	        //Act
	        var result = logic.CreateUser(user);
	        
	        //Assert
	        Assert.AreEqual(user, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User with email abc@abc.com already exists")]
        public void CreateUser_InvalidEmail_FAIL()
        {
	        //Arrange
	        var user = new User
	        {
		        Email = "abc@abc.com",
		        Password = "Password123",
		        Address = "Ejido 1234",
		        Role = Role.Comprador
	        };
			
	        var mock = new Mock<IUserRepository>(MockBehavior.Strict);
	        mock.Setup(ur => ur.FindUser(It.IsAny<string>())).Returns(true);

	        var logic = new UserLogic(mock.Object);
	        //Act
	        logic.CreateUser(user);
	        
	        //ERROR
        }

        [TestMethod]
        public void UpdateUser_ValidUser_OK()
        {
	        //Arrange
	        var user = new User
	        {
		        Email = "a@a.a",
		        Password = "Password123",
		        Role = Role.Administrador
	        };
	        
	        var mock = new Mock<IUserRepository>(MockBehavior.Strict);
	        mock.Setup(ur =>
                ur.UpdateUser(It.IsAny<Guid>(), It.IsAny<User>()))
		        .Returns(user);

	        var logic = new UserLogic(mock.Object);
	        
	        //Act
	        var result = logic.UpdateUser(Guid.Empty, user);
	        
	        //Assert
	        Assert.AreEqual(user, result);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User with id 0 not found")]
        public void UpdateUser_InvalidUser_FAIL()
		{
	        //Arrange
	        var user = new User
	        {
		        Email = "a@a.a",
		        Password = "Password123",
		        Role = Role.Administrador
	        };
	        
	        var mock = new Mock<IUserRepository>(MockBehavior.Strict);
	        mock.Setup(ur =>
			        ur.UpdateUser(It.IsAny<Guid>(), It.IsAny<User>()))
		        .Throws(new ArgumentException("User with id 0 not found"));

	        var logic = new UserLogic(mock.Object);
	        
	        //Act
	        logic.UpdateUser(Guid.Empty, user);
	        
	        //ERROR
		}
	}
}
