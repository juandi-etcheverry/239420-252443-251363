using ApiModels.Requests;
using Domain;
using Logic;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Controllers.Products;

namespace Controller.Test
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void CreateProduct_OK_Test()
        {
            var request = new CreateProductRequest
            {
                Name = "Product 1",
                Price = 100,
                Description = "Description",
                Brand = new Brand() { Name = "Brand" },
                Category = new Category() { Name = "Category" },
                Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
            };

            var mockProduct = new Mock<IProductLogic>(MockBehavior.Strict);
            mockProduct.Setup(x => x.AddProduct(It.IsAny<Product>())).Returns(request.ToEntity());
            var mockSession = new Mock<ISessionTokenLogic>(MockBehavior.Strict);

            //q: what is the problem?
            //a: the problem is that the controller is not instantiated with the mock objects
            var controller = new ProductsController(mockProduct.Object, mockSession.Object);
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

            var mockProduct = new Mock<IProductLogic>(MockBehavior.Strict);
            mockProduct.Setup(x => x.GetProducts(It.IsAny<Func<Product, bool>>())).Returns(() =>
            {
                return new List<Product>()
                {
                    new Product()
                    {
                        Brand = new Brand() { Name = "Nike" },
                        Category = new Category() { Name = "Shoes" },
                        Name = "Air Max",
                        Description = "Air Max 90",
                        Colors = new List<Color> { new() { Name = "Red" }, new() { Name = "Blue" } }
                    }
                };
            });
            var mockSession = new Mock<ISessionTokenLogic>(MockBehavior.Strict);

            var controller = new ProductsController(mockProduct.Object, mockSession.Object);
            var result = controller.GetProducts(request) as ObjectResult;

            Assert.AreEqual(200, result?.StatusCode);
        }
    }
}
