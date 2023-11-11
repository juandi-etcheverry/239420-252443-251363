using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Tests
{
    [TestClass]
    public class ColorRepositoryTest
    {
        private DbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(dbName).Options;
            return new Context(options);
        }

        [TestMethod]
        public void GetColors_OK()
        {
            var context = CreateDbContext("GetColors_OK");
            var colorRepository = new ColorRepository(context);
            var color1 = new Color
            {
                Name = "Red",
            };
            var color2 = new Color
            {
                Name = "Blue",
            };
            context.Set<Color>().Add(color1);
            context.Set<Color>().Add(color2);
            context.SaveChanges();

            IList<Color> result = colorRepository.GetAllColors();

            Assert.AreEqual(2, result.Count);
        }
    }
}
