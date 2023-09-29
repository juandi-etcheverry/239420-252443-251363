using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ApiModels.Requests.Users;
using TypeHelper;
using WebApi.Controllers.Users;

namespace Controller.Test
{
    [TestClass]
    public class UserTest
    {

        [TestMethod]
        public void GetUserAsAdmin_OK()
        {
            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(logic => logic.GetUser(It.IsAny<Guid>())).Returns(new User()
            {
                Email = "testing@gmail.com",
                Address = "Miramar 1223",
                Password = "Password123",
                Role = Role.Comprador
            });

            var controller = new UsersController(mock.Object);

            var response = controller.GetUser(new Guid()) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public void DeleteUserAsAdmin_OK()
        {
            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(logic => logic.DeleteUser(It.IsAny<Guid>())).Returns(new User()
            {
                Email = "testing@gmail.com",
                Address = "Miramar 1223",
                Password = "Password123",
                Role = Role.Comprador,
                IsDeleted = true
            });

            var controller = new UsersController(mock.Object);

            var response = controller.DeleteUser(new Guid()) as ObjectResult;

            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public void CreateUserAsAdmin_OK()
        {
            var admin = new User()
            {
                Email = "test@gmail.com",
                Address = "Mercedes 2331",
                Password = "Password123",
                Role = Role.Administrador
            };

            var mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(admin);

            var mockSession = new Mock<ISessionTokenLogic>(MockBehavior.Strict);
            mockSession.Setup(x => x.GetSessionToken(It.IsAny<Guid>())).Returns(new SessionToken()
            {
                User = admin
            });

            var controller = new CreateUserController(mock.Object, mockSession.Object);

            var request = new CreateUserRequest()
            {
                Email = "noImporta@test.com",
                Address = "Mercedes 2331",
                Password = "NoSoySeguro123",
                PasswordConfirmation = "NoSoySeguro123"
            };

            var response = controller.CreateUser(request, new Guid()) as ObjectResult;

            Assert.AreEqual(201, response.StatusCode);
        }
    }
}
