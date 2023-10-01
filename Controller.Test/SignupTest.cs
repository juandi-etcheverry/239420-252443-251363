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

            var mockSession = new Mock<ISessionTokenLogic>(MockBehavior.Strict);
            var sessionToken = new SessionToken() { };

            mockSession.Setup(x => x.AddSessionToken(It.IsAny<SessionToken>())).Returns(sessionToken);
            mockSession.Setup(x => x.GetSessionToken(It.IsAny<Guid>())).Returns(sessionToken);


            SignupRequest request = new SignupRequest()
            {
                Email = "user1@gmail.com",
                Address = "Miramar 1223",
                Password = "Password123",
                PasswordConfirmation = "Password123"
            };

            var controller = new SignupController(mock.Object, mockSession.Object);

            // Act
            var response = controller.Signup(request) as ObjectResult;

            // Assert
            Assert.AreEqual(201, response.StatusCode);
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

            var mockSession = new Mock<ISessionTokenLogic>(MockBehavior.Strict);
            var sessionToken = new SessionToken()
            {
                User = new User()
                {
                    Email = "abc@test.com",
                    Address = "Miramar 1223",
                    Password = "Password123",
                }
            };
            mockSession.Setup(x => x.AddSessionToken(It.IsAny<SessionToken>())).Returns(sessionToken);

            SignupRequest request = new SignupRequest()
            {
                Email = "user1@gmail.com",
                Address = "Miramar 1223",
                Password = "Password123",
                PasswordConfirmation = "Password123"
            };

            var controller = new SignupController(mock.Object, mockSession.Object);

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

            var mockSession = new Mock<ISessionTokenLogic>(MockBehavior.Strict);
            var sessionToken = new SessionToken()
            {
                User = new User()
                {
                    Email = "abc@test.com",
                    Address = "Miramar 1223",
                    Password = "Password123",
                }
            };
            mockSession.Setup(x => x.AddSessionToken(It.IsAny<SessionToken>())).Returns(sessionToken);

            SignupRequest request = new SignupRequest()
            {
                Email = "user1@gmail.com",
                Address = "Miramar 1223",
                Password = "Password123",
                PasswordConfirmation = "Password123"
            };

            var controller = new SignupController(mock.Object, mockSession.Object);

            var response = controller.Signup(request) as ObjectResult;


            Assert.AreEqual(400, response.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User email can't be empty")]
        public void Signup_Controller_NoPassword_FAIL()
        {
            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(new User()
            {
                Email = "test@test.com",
                Address = "Miramar 1223",
                Password = "",
                Role = Role.Comprador
            });

            var mockSession = new Mock<ISessionTokenLogic>(MockBehavior.Strict);
            var sessionToken = new SessionToken()
            {
                User = new User()
                {
                    Email = "abc@test.com",
                    Address = "Miramar 1223",
                    Password = "Password123",
                }
            };
            mockSession.Setup(x => x.AddSessionToken(It.IsAny<SessionToken>())).Returns(sessionToken);

            SignupRequest request = new SignupRequest()
            {
                Email = "user1@gmail.com",
                Address = "Miramar 1223",
                Password = "Password123",
                PasswordConfirmation = "Password123"
            };

            var controller = new SignupController(mock.Object, mockSession.Object);

            var response = controller.Signup(request) as ObjectResult;


            Assert.AreEqual(400, response.StatusCode);
        }

        [TestMethod]
        public void SignupController_AlreadyLoggedIn_FAIL()
        {
            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(new User()
            {
                Email = "test@test.com",
                Address = "Miramar 1223",
                Password = "Password123",
                Role = Role.Comprador
            });

            var mockSession = new Mock<ISessionTokenLogic>(MockBehavior.Strict);
            var sessionToken = new SessionToken()
            {
                User = new User()
                {
                    Email = "test@test.com",
                    Address = "Miramar 1223",
                    Password = "Password123",
                }
            };
            mockSession.Setup(x => x.GetSessionToken(It.IsAny<Guid>())).Returns(sessionToken);

            SignupRequest request = new SignupRequest()
            {
                Email = "user1@gmail.com",
                Address = "Miramar 1223",
                Password = "Password123",
                PasswordConfirmation = "Password123"
            };

            var controller = new SignupController(mock.Object, mockSession.Object);

            var response = controller.Signup(request) as ObjectResult;


            Assert.AreEqual(400, response.StatusCode);
        }
    }
}