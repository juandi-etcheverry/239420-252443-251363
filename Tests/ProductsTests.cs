using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiModels.Requests;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace Tests
{
    [TestClass]
    public class ProductsTests
    {
        [TestMethod]
        public void CreateProduct_OK_Test()
        {
            var request = new CreateProductRequest
            {
                Name = "Product 1",
                Price = 100,
                Description = "Description",
                Brand = "Brand",
                Category = "Category",
                Colors = new List<string> { "Red", "Blue" }
            };

            var controller = new ProductsController();
            var result = controller.CreateProduct(request) as ObjectResult;

            Assert.AreEqual(201, result?.StatusCode);
        }


        [TestMethod]
        public void GetProducts_OK_Test()
        {
            var request = new GetProductsRequest()
            {
                Brand = "Nike",
                Category = "Shoes",
                Text = "Air"
            };

            var controller = new ProductsController();
            var result = controller.GetProducts(request) as ObjectResult;

            Assert.AreEqual(200, result?.StatusCode);
        }
    }
}
