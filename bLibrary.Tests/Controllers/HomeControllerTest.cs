using bLibrary.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace bLibrary.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        [DataRow(2)]
        [DataRow(null)]
        public async Task MainPage(int? value)
        {
            HomeController homeController = new HomeController();
            ViewResult viewResult = await homeController.MainPage(value) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        [DataRow("name", 2)]
        [DataRow("", null)]
        public async Task Search(string name, int? value)
        {
            HomeController homeController = new HomeController();
            ViewResult viewResult = await homeController.Search(name, value) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}
