using ApiModels.Requests.Users;
using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TypeHelper;
using WebApi.Controllers.Users;

namespace Controller.Test
{
    [TestClass]
    public class SignupTest
    {
        [TestMethod]
        public void Signup_Controller_OK()
        {
            // Arrange
            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(new User()
            {
                Address = "Miramar 1223",
                Email = "testing@gmail.com",
                Password = "Password123",
                Role = Role.Comprador
            });

            SignupRequest request = new SignupRequest()
            {
                Email = "user1@gmail.com",
                Address = "Miramar 1223",
                Password = "Password123",
                PasswordConfirmation = "Password123"
            };

            var controller = new SignupController(mock.Object);

            // Act
            var response = controller.Signup(request) as ObjectResult;

            // Assert
            Assert.AreEqual(response.StatusCode, 201);
        }
    }   
}