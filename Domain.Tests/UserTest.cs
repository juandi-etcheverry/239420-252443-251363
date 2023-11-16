using TypeHelper;

namespace Domain.Tests;

[TestClass]
public class UserTest
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "User email can't be empty")]
    public void NewUser_EmptyEmail_FAIL()
    {
        var user = new User
        {
            Email = "",
            Password = "Password123",
            Role = Role.Buyer,
            Address = "Ejido 1234"
        };
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "User email format is incorrect")]
    public void NewUser_WrongEmailFormat_FAIL()
    {
        var user = new User
        {
            Email = "userEmail",
            Password = "Password123",
            Role = Role.Buyer,
            Address = "Ejido 1234"
        };
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "User password can't be empty")]
    public void NewUser_PasswordEmpty_FAIL()
    {
        var user = new User
        {
            Email = "user@test.com",
            Password = "",
            Role = Role.Buyer,
            Address = "Ejido 1234"
        };
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "User password must be at least 5 characters")]
    public void NewUser_PasswordTooShort_FAIL()
    {
        var user = new User
        {
            Email = "user@test.com",
            Password = "1234",
            Role = Role.Buyer,
            Address = "Ejido 1234"
        };
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "User address can't be empty")]
    public void NewUser_AddressEmpty_FAIL()
    {
        var user = new User
        {
            Email = "user@test.com",
            Password = "Password123",
            Role = Role.Buyer,
            Address = ""
        };
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "User email format is incorrect")]
    public void NewUser_EmailWithoutAt_FAIL()
    {
        var user = new User
        {
            Email = "wrongEmailgmail.com",
            Password = "",
            Address = "Okay address"
        };
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException), "User email format is incorrect")]
    public void NewUser_EmailWithoutDot_FAIL()
    {
        var user = new User
        {
            Email = "wrongEmail@gmailcom",
            Password = "",
            Address = "Okay address"
        };
    }
}