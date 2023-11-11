using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Domain;
using Logic.Interfaces;
using Moq;

namespace Logic.Tests
{
    [TestClass]
    public class BrandLogicTest
    {
        [TestMethod]
        public void GetAllBrands_OK()
        {
            // Arrange
            var mock = new Mock<IBrandRepository>();
            var brandLogic = new BrandLogic(mock.Object);
            var expected = new List<Brand>
            {
                new Brand { Name = "Brand 1" },
                new Brand { Name = "Brand 2" },
                new Brand { Name = "Brand 3" }
            };
            mock.Setup(x => x.GetAllBrands()).Returns(expected);

            // Act
            var actual = brandLogic.GetAllBrands();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
