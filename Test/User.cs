using NUnit.Framework;
using API.Controllers;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class User
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Login()
        {
            var controller = new userController();
            string args = "{userName:Deepak;password:Test}";
            var token = controller.Login(args);
            string strg;
            strg = string.IsNullOrEmpty(token) ? "Token is Empty" : "Token is not empty";
            Assert.AreSame("Token is not empty", strg);
        }
    }
}