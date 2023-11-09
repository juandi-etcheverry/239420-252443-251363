using Domain;
using Microsoft.EntityFrameworkCore;
using TypeHelper;

namespace DataAccess.Tests;

[TestClass]
public class Test
{
    private Brand _brand;
    private Category _category;
    private Color _color;
    private Product _product;
    private User _user;

    private DbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(dbName).Options;
        var context = new Context(options);

        _brand = new Brand
        {
            Name = "Gucci"
        };
        context.Set<Brand>().Add(_brand);

        _category = new Category
        {
            Name = "Bag"
        };
        context.Set<Category>().Add(_category);

        _color = new Color
        {
            Name = "Red"
        };
        context.Set<Color>().Add(_color);

        _product = new Product
        {
            Brand = _brand,
            Category = _category,
            Colors = new List<Color> { _color },
            Description = "Test Description",
            Name = "Test Product",
            Price = 100
        };
        context.Set<Product>().Add(_product);

        _user = new User
        {
            Email = "test@test.com",
            Address = "Test Address",
            Password = "Test Password",
            Role = Role.Buyer
        };
        context.Set<User>().Add(_user);


        return context;
    }

    [TestMethod]
    public void AddCart_CorrectCart_OK()
    {
        //Arrange
        var context = CreateDbContext("AddCart_CorrectCart");
        var cartRepository = new PurchaseRepository(context);
        var session = new SessionToken();
        var cart = new Purchase
        {
            FinalPrice = 100,
            TotalPrice = 100,
            PromotionName = "Test Promotion",
            Products = new List<Product> { _product },
            User = _user
        };
        //Act
        var result = cartRepository.AddPurchase(cart);

        //Assert
        Assert.AreEqual(cart, result);
    }

    [TestMethod]
    public void AddProduct_CorrectProductOK()
    {
        //Arrange
        var context = CreateDbContext("AddProduct_CorrectProductOK");
        var cartRepository = new PurchaseRepository(context);
        var products = new List<Product>();
        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = _brand,
            Category = _category,
            Colors = new List<Color> { _color }
        };
        products.Add(product);
        context.Set<Product>().Add(product);
        context.SaveChanges();
        var session = new SessionToken();
        var cart = new Purchase
        {
            FinalPrice = 100,
            TotalPrice = 100,
            PromotionName = "Test Promotion",
            Products = new List<Product> { product },
            User = _user
        };
        var cartResult = cartRepository.AddPurchase(cart);

        //Act
        var result = cartRepository.AddProducts(cartResult, products);

        //Assert
        Assert.AreEqual(result.Products[0].Id, product.Id);
    }

    [TestMethod]
    public void AddTwoProducts_CorrectProduct()
    {
        //Arrange
        var context = CreateDbContext("AddTwoProducts_CorrectProduct");
        var cartRepository = new PurchaseRepository(context);
        var products = new List<Product>();
        var product1 = new Product
        {
            Name = "Test Product1",
            Description = "Test Description",
            Price = 100,
            Brand = _brand,
            Category = _category,
            Colors = new List<Color> { _color }
        };
        products.Add(product1);
        context.Set<Product>().Add(product1);
        context.SaveChanges();

        var product2 = new Product
        {
            Name = "Test Product2",
            Description = "Test Description2",
            Price = 100,
            Brand = _brand,
            Category = _category,
            Colors = new List<Color> { _color }
        };
        products.Add(product2);
        context.Set<Product>().Add(product2);
        context.SaveChanges();
        var session = new SessionToken();
        var cart = new Purchase
        {
            FinalPrice = 100,
            TotalPrice = 100,
            PromotionName = "Test Promotion",
            Products = new List<Product> { product1, product2 },
            User = _user
        };
        var cartResult = cartRepository.AddPurchase(cart);

        //Act
        var result = cartRepository.AddProducts(cartResult, products);

        //Assert
        Assert.AreEqual(result.Products.Count, 2);
    }

    [TestMethod]
    public void AddTwoProductsEqual_OK()
    {
        //Arrange
        var context = CreateDbContext("AddTwoProducts_CorrectProduct");
        var cartRepository = new PurchaseRepository(context);
        var products = new List<Product>();
        var product1 = new Product
        {
            Name = "Test Product1",
            Description = "Test Description",
            Price = 100,
            Brand = _brand,
            Category = _category,
            Colors = new List<Color> { _color }
        };
        products.Add(product1);
        context.Set<Product>().Add(product1);
        context.SaveChanges();
        var session = new SessionToken();
        var cart = new Purchase
        {
            FinalPrice = 100,
            TotalPrice = 100,
            PromotionName = "Test Promotion",
            Products = new List<Product> { product1 },
            User = _user
        };
        var cartResult = cartRepository.AddPurchase(cart);

        //Act
        products.Add(product1);
        var result = cartRepository.AddProducts(cartResult, products);

        //Assert
        Assert.AreEqual(result.Products.Count, 1);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Product is null")]
    public void AddProduct_NullProductFail()
    {
        //Arrange
        var context = CreateDbContext("AddProduct_CorrectProductOK");
        var cartRepository = new PurchaseRepository(context);
        var session = new SessionToken();
        var cart = new Purchase
        {
            FinalPrice = 100,
            TotalPrice = 100,
            PromotionName = "Test Promotion",
            Products = new List<Product>(),
            User = _user
        };
        var cartResult = cartRepository.AddPurchase(cart);
        var products = new List<Product>();
        Product product = null;
        products.Add(product);

        //Act
        var result = cartRepository.AddProducts(cartResult, products);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "There are no products to Add")]
    public void AddProduct_NoProducts_Fail()
    {
        //Arrange
        var context = CreateDbContext("AddProduct_NoProducts_Fail");
        var cartRepository = new PurchaseRepository(context);
        var session = new SessionToken();
        var purchase = new Purchase
        {
            FinalPrice = 100,
            TotalPrice = 100,
            PromotionName = "Test Promotion",
            Products = new List<Product>(),
            User = _user
        };
        var cartResult = cartRepository.AddPurchase(purchase);
        var products = new List<Product>();

        //Act
        var result = cartRepository.AddProducts(cartResult, products);
    }

    [TestMethod]
    public void DeleteProduct_CorrectProductOK()
    {
        //Arrange
        var context = CreateDbContext("DeleteProduct_CorrectProductOK");
        var cartRepository = new PurchaseRepository(context);
        var products = new List<Product>();
        var product = new Product
        {
            Name = "Test Product1",
            Description = "Test Description",
            Price = 100,
            Brand = _brand,
            Category = _category,
            Colors = new List<Color> { _color }
        };
        products.Add(product);
        context.Set<Product>().Add(product);
        context.SaveChanges();
        var session = new SessionToken();
        var cart = new Purchase
        {
            FinalPrice = 100,
            TotalPrice = 100,
            PromotionName = "Test Promotion",
            Products = new List<Product> { product },
            User = _user
        };
        var cartResult = cartRepository.AddPurchase(cart);
        var cartResultWithProduct = cartRepository.AddProducts(cartResult, products);

        //Act
        var result = cartRepository.DeleteProduct(cartResultWithProduct, product);

        //Assert
        Assert.AreEqual(result.Products.Count, 0);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Product is null")]
    public void DeleteProduct_NullProductFail()
    {
        //Arrange
        var context = CreateDbContext("DeleteProduct_NullProductFail");
        var cartRepository = new PurchaseRepository(context);
        var session = new SessionToken();
        var cart = new Purchase
        {
            FinalPrice = 100,
            TotalPrice = 100,
            PromotionName = "Test Promotion",
            Products = new List<Product>(),
            User = _user
        };
        var cartResult = cartRepository.AddPurchase(cart);
        Product product = null;

        //Act
        var result = cartRepository.DeleteProduct(cartResult, product);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Cart is null")]
    public void AddProduct_NullCartFail()
    {
        //Arrange
        var context = CreateDbContext("AddProduct_NullCartFail");
        var cartRepository = new PurchaseRepository(context);
        var products = new List<Product>();
        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        products.Add(product);
        context.Set<Product>().Add(product);
        context.SaveChanges();
        Purchase purchase = null;

        //Act
        var result = cartRepository.AddProducts(purchase, products);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "Cart is null")]
    public void DeleteProduct_NullCartFail()
    {
        //Arange
        var context = CreateDbContext("DeleteProduct_NullCartFail");
        var cartRepository = new PurchaseRepository(context);
        var product = new Product
        {
            Name = "Test Product",
            Description = "Test Description",
            Price = 100,
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        context.Set<Product>().Add(product);
        context.SaveChanges();
        Purchase purchase = null;

        //Act
        var result = cartRepository.DeleteProduct(purchase, product);
    }

    [TestMethod]
    public void AssignUserToPurchase_OK()
    {
        var context = CreateDbContext("DeleteProduct_NullCartFail");
        var purchaseRepository = new PurchaseRepository(context);
        var user = new User();
        var purchase = new Purchase
        {
            FinalPrice = 100,
            TotalPrice = 100,
            PromotionName = "Test Promotion",
            Products = new List<Product>(),
            User = _user
        };
        var purchaseResult = purchaseRepository.AddPurchase(purchase);

        //Act
        purchaseRepository.AssignUserToPurchase(purchaseResult, user);

        //Assert
        Assert.AreEqual(purchaseResult.User, user);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "User is null")]
    public void AssignUserToPurchase_NullUser_OK()
    {
        var context = CreateDbContext("DeleteProduct_NullCartFail");
        var purchaseRepository = new PurchaseRepository(context);
        var purchase = new Purchase
        {
            FinalPrice = 100,
            TotalPrice = 100,
            PromotionName = "Test Promotion",
            Products = new List<Product>(),
            User = _user
        };
        var purchaseResult = purchaseRepository.AddPurchase(purchase);

        //Act
        purchaseRepository.AssignUserToPurchase(purchaseResult, null);
    }

    [TestMethod]
    public void GetAllPurchasesHistory_Completed_OK()
    {
        //Arrange
        var context = CreateDbContext("GetAllPurchasesHistory");
        var purchaseRepository = new PurchaseRepository(context);

        var purchase = new Purchase
        {
            User = _user
        };
        var purchaseResult = purchaseRepository.AddPurchase(purchase);

        //Act
        var result = purchaseRepository.GetAllPurchasesHistory(_user);

        //Assert
        Assert.AreEqual(1, result.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "User is null")]
    public void GetAllPurchasesHistory_NullUser_Fail()
    {
        //Arrange
        var context = CreateDbContext("GetAllPurchasesHistory_NullUser_Fail");
        var purchaseRepository = new PurchaseRepository(context);
        var purchase = new Purchase
        {
            FinalPrice = 100,
            TotalPrice = 100,
            PromotionName = "Test Promotion",
            Products = new List<Product>(),
            User = _user
        };
        var purchaseResult = purchaseRepository.AddPurchase(purchase);

        //Act
        var result = purchaseRepository.GetAllPurchasesHistory(null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "There are no purchases")]
    public void GetAllPurchasesHistory_NoPurchases_Fail()
    {
        //Arrange
        var context = CreateDbContext("GetAllPurchasesHistory_NoPurchases_Fail");
        var purchaseRepository = new PurchaseRepository(context);
        var user = new User
        {
            Email = "test@gmail.com",
            Address = "Cuareim123",
            Password = "Cuareim123"
        };

        //Act
        var result = purchaseRepository.GetAllPurchasesHistory(user);
    }

    [TestMethod]
    public void CalculateTotalPrice_OK()
    {
        //Arrange
        var context = CreateDbContext("CalculateTotalPrice_OK");
        var purchaseRepository = new PurchaseRepository(context);
        var user = new User();
        var products = new List<Product>();
        var product1 = new Product
        {
            Price = 300
        };
        var product2 = new Product
        {
            Price = 200
        };
        products.Add(product1);
        products.Add(product2);
        var purchase = new Purchase
        {
            User = user
        };

        //Act
        var result = purchaseRepository.AddProducts(purchase, products);

        //Assert
        Assert.AreEqual(500, result.TotalPrice);
    }

    [TestMethod]
    public void GetAllPurchasesHistory_AllPurchases_OK()
    {
        //Arrange
        var context = CreateDbContext("GetAllPurchasesHistory_AllPurchases_OK");
        var purchaseRepository = new PurchaseRepository(context);
        
        var user = new User
        {
            Email = "A@A.A",
            Address = "A",
            Password = "Password123",
            Role = Role.Buyer
        };
        var products = new List<Product>();
        var product1 = new Product
        {
            Name = "A",
            Description = "A",
            Price = 300,
            Id = Guid.NewGuid(),
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        var product2 = new Product
        {
            Name = "A",
            Description = "A",
            Price = 200,
            Id = Guid.NewGuid(),
            Brand = new Brand { Name = "Gucci" },
            Category = new Category { Name = "Bag" },
            Colors = new List<Color> { new() { Name = "Red" } }
        };
        products.Add(product1);
        products.Add(product2);
        var purchase = new Purchase
        {
            User = user,
            Products = products
        };
        var productsRepo = new ProductRepository(context);
        productsRepo.AddProduct(product1);
        productsRepo.AddProduct(product2);
        var userRepo = new UserRepository(context);
        userRepo.AddUser(user);
        purchaseRepository.AddProducts(purchase, products);
        purchaseRepository.AddPurchase(purchase);

        //Act
        var result = purchaseRepository.GetAllPurchasesHistory();

        //Assert
        Assert.AreEqual(purchase, result[0]);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "There are no purchases")]
    public void GetAllPurchasesHistory_AllPurchases_Fail()
    {
        //Arrange
        var context = CreateDbContext("GetAllPurchasesHistory_AllPurchases_Fail");
        var purchaseRepository = new PurchaseRepository(context);

        //Act
        var result = purchaseRepository.GetAllPurchasesHistory();
    }
}