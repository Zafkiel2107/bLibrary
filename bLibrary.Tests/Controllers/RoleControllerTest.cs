using bLibrary.Controllers;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace bLibrary.Tests.Controllers
{
    [TestClass]
    public class RoleControllerTest
    {
        [TestMethod]
        [DataRow("32f26f7e-6eb3-444c-868a-3d7f6d4fdd14")]
        public async Task GetEditRole(string value)
        {
            RoleController roleController = new RoleController();
            ViewResult viewResult = await roleController.GetEditRole(value) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        public async Task EditRole()
        {
            IdentityUserRole identityUserRole = new IdentityUserRole
            {
                RoleId = "1",
                UserId = "32f26f7e-6eb3-444c-868a-3d7f6d4fdd14"
            };
            RoleController roleController = new RoleController();
            ViewResult viewResult = await roleController.EditRole(identityUserRole) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}
