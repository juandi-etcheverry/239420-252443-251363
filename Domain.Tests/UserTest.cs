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
    }
}