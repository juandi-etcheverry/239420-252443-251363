using ApiModels.Requests.Users;
using Microsoft.AspNetCore.Mvc;
using TypeHelper;
using WebApi.Controllers.Users;

namespace Tests;

[TestClass]
public class SignupTests
{
    
    
    [TestMethod]
    public void Signup_OK_Test()
    {
        var request = new SignupRequest
        {
            Email = "example@example.com",
            Password = "Password123",
            PasswordConfirmation = "Password123",
        };

        var controller = new SignupController();
        var result = controller.Signup(request) as ObjectResult;

        Assert.AreEqual(201, result?.StatusCode);
    }
}