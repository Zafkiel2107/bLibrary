using bLibrary.Controllers;
using bLibrary.DBContext;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace bLibrary.Tests.Controllers
{
    [TestClass]
    public class LayoutControllerTest
    {
        [TestMethod]
        public async Task AdminPanel()
        {
            LayoutController homeController = new LayoutController();
            ViewResult viewResult = await homeController.AdminPanel() as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        public async Task Settings()
        {
            LayoutController homeController = new LayoutController();
            ViewResult viewResult = await homeController.Settings() as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        public async Task EditSettings()
        {
            IdentityUser identityUser = new IdentityUser
            {
                AccessFailedCount = 0,
                Email = "AspNet@gmail.com",
                EmailConfirmed = false,
                Id = "32f26f7e-6eb3-444c-868a-3d7f6d4fdd14",
                LockoutEnabled = false,
                PasswordHash = "AIIpHWPI1VWtOqrSnkijedA9ecfJYLo3mJNOegrlQDs4g9IKv92/uD10WCSN9JL+tA==",
                PhoneNumber = "89178889193",
                PhoneNumberConfirmed = false,
                SecurityStamp = "75c9aaba-8374-441b-974b-38aa16b6b0e2",
                TwoFactorEnabled = false,
                UserName = "AspNet"
            };
            LayoutController layoutController = new LayoutController();
            ViewResult viewResult = await layoutController.EditSettings(identityUser) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}
