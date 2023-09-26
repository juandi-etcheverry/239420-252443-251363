using ApiModels.Requests.Users;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TypeHelper;

namespace Controller.Test
{
    [TestClass]
    public class SignupTest
    {
        [TestMethod]
        public void Signup_Controller_OK()
        {
            // Arrange
            var mock = new Mock<IUserLogic>();
            mock.Setup(x => x.CreateUser(It.IsAny<User>())).Returns(new User()
            {
                Address = "Miramar 1223",
                Email = "testing@gmail.com",
                Role = Role.Comprador
            });

            SignupRequest request = new SignupRequest()
            {
                Email = "user1@gmail.com",
                Password = "Password123",
                PasswordConfirmation = "Password123"
            };

            var controller = new SignupController(mock.Object);

            // Act
            var response = controller.Signup(request) as ObjectResult;

            // Assert
            Assert.IsInstanceOfType(response, typeof(StatusCodeResult));
            Assert.AreEqual(response.StatusCode, 201);
        }
    }
}