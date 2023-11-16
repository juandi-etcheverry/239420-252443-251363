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
    public class ColorLogicTest
    {
        [TestMethod]
        public void GetAllColors_OK()
        {
            IList<Color> colors = new List<Color>
            {
                new Color
                {
                    Name = "Red"
                },
                new Color
                {
                    Name = "Blue"
                }
            };

            var mock = new Mock<IColorRepository>(MockBehavior.Strict);
            mock.Setup(x => x.GetAllColors()).Returns(colors);
            var logic = new ColorLogic(mock.Object);
            var result = logic.GetAllColors();

            Assert.AreEqual(colors, result);
        }
    }
}
