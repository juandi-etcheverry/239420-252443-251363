using ApiModels.Requests;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

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
            Roles = new[] { "Admin" }
        };

        var controller = new SignupController();
        var result = controller.Signup(request) as ObjectResult;

        Assert.AreEqual(201, result?.StatusCode);
    }
}