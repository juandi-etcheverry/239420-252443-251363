using System;
using DataAccess.Interfaces;
using Domain;
using Moq;
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
				Role = Role.Comprador,
				Address = "Ejido 1234"
			};
			var mock = new Mock<IUserRepository>(MockBehavior.Strict);
			mock.Setup(x => x.GetUser(user.Id)).Returns(user);
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
                Role = Role.Comprador,
                Address = "Ejido 1234"
            };
            var mock = new Mock<IUserRepository>(MockBehavior.Strict);
            mock.Setup(x => x.GetUser(user.Id)).Throws(new ArgumentException("User with id 0 not found"));
			var logic = new UserLogic(mock.Object);

			//Act
			var result = logic.GetUser(user.Id);
        }
	}
}

