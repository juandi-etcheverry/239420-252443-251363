using TypeHelper;

namespace Domain.Tests
{
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
                Role = Role.Comprador,
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
                Role = Role.Comprador,
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
                Role = Role.Comprador,
                Address = "Ejido 1234"
            };
        }
    }
}