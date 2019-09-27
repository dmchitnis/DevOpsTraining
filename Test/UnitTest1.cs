using NUnit.Framework;
using myapi.Controllers;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var controller = new ValuesController();
            int args = 1;
            var retval = controller.Get(args);
            string retstrg = retval.Value.ToString();
            string strg = "{\"Value\": \"value\"}";
            strg = strg.CompareTo(retstrg) == 0 ? "Return value is valid" : "Return value is invalid";
            Assert.AreSame("Return value is valid", strg);
        }
    }
}