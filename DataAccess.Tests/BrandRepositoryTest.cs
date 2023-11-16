using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DataAccess.Tests
{
    [TestClass]
    public class BrandRepositoryTest
    {
        private DbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(dbName).Options;
            return new Context(options);
        }

        [TestMethod]
        public void GetBrands_OK()
        {
            var context = CreateDbContext("GetBrands_OK");
            var brandRepository = new BrandRepository(context);
            var brand1 = new Brand
            {
                Name = "Red",
            };
            var brand2 = new Brand
            {
                Name = "Blue",
            };
            context.Set<Brand>().Add(brand1);
            context.Set<Brand>().Add(brand2);
            context.SaveChanges();

            IList<Brand> result = brandRepository.GetAllBrands();

            Assert.AreEqual(2, result.Count);
        }
    }
}
