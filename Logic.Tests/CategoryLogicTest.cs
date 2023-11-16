using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Domain;
using Moq;

namespace Logic.Tests
{
    [TestClass]
    public class CategoryLogicTest
    {
        [TestMethod]
        public void GetAllCategories_OK()
        {
            IList<Category> categories = new List<Category>
            {
                new Category
                {
                    Name = "Red"
                },
                new Category
                {
                    Name = "Blue"
                }
            };

            var mock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            mock.Setup(x => x.GetAllCategories()).Returns(categories);
            var logic = new CategoryLogic(mock.Object);
            var result = logic.GetAllCategories();

            Assert.AreEqual(categories, result);
        }
    }
}
