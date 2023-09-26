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
                Email = "testing@gmail.com",
                Address = "Miramar 1223",
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User email can't be empty")]
        public void Signup_Controller_NoEmail_FAIL()
        {
            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(new User()
            {
                Email = "",
                Address = "Miramar 1223",
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

            var response = controller.Signup(request) as ObjectResult;

            
            Assert.AreEqual(400, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User email can't be empty")]
        public void Signup_Controller_NoAddress_FAIL()
        {
            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(new User()
            {
                Email = "test@test.com",
                Address = "",
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

            var response = controller.Signup(request) as ObjectResult;


            Assert.AreEqual(400, response.StatusCode);
        }
    }   
}