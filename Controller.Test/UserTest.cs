using Domain;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
