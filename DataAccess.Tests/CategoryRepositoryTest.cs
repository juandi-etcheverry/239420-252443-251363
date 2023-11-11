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
    public class CategoryRepositoryTest
    {
        private DbContext CreateDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(dbName).Options;
            return new Context(options);
        }

        [TestMethod]
        public void GetCategories_OK()
        {
            var context = CreateDbContext("GetCategories_OK");
            var categoryRepository = new CategoryRepository(context);
            var category1 = new Category
            {
                Name = "Red",
            };
            var category2 = new Category
            {
                Name = "Blue",
            };
            context.Set<Category>().Add(category1);
            context.Set<Category>().Add(category2);
            context.SaveChanges();

            IList<Category> result = categoryRepository.GetAllCategories();

            Assert.AreEqual(2, result.Count);
        }
    }
}
