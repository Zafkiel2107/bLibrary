using bLibrary.Controllers;
using bLibrary.Models.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace bLibrary.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public async Task Register()
        {
            RegisterModel registerModel = new RegisterModel
            {
                Age = 18,
                UserName = "AspNetUser",
                Email = "AspNetUser@gmail.com",
                Password = "qwerty",
                ConfirmPassword = "qwerty"
            };
            AccountController accController = new AccountController();
            ViewResult viewResult = await accController.Register(registerModel) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        [DataRow("/Home/MainPage")]
        public async Task Login(string returnUrl)
        {
            LoginModel loginModel = new LoginModel
            {
                UserName = "AspNetUser",
                Password = "qwerty"
            };
            AccountController accController = new AccountController();
            ViewResult viewResult = await accController.Login(loginModel, returnUrl) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        public async Task ForgotPassword()
        {
            ForgotPasswordModel forgotPasswordModel = new ForgotPasswordModel
            {
                Email = "AspNetUser@gmail.com"
            };
            AccountController accController = new AccountController();
            ViewResult viewResult = await accController.ForgotPassword(forgotPasswordModel) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        public async Task ResetPassword()
        {
            ResetPasswordModel resetPasswordModel = new ResetPasswordModel
            {
                Email = "AspNetUser@gmail.com",
                Password = "admin",
                ConfirmPassword = "admin",
                Code = "qwerqwerqwer"
            };
            AccountController accController = new AccountController();
            ViewResult viewResult = await accController.ResetPassword(resetPasswordModel) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}
