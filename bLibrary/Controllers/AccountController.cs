using bLibrary.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace bLibrary.Controllers
{
    public class AccountController : Controller
    {
        private AppUserManager AppUserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }
        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ResetPassword(string code)
        {
            if(code == null)
            {

            }
            return View(new ResetPasswordModel { Code = code });
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = registerModel.UserName,
                    Email = registerModel.Email,
                    Age = registerModel.Age,
                    PasswordHash = GetPasswordHash(registerModel.Password)
                };
                IdentityResult identityUserResult = await AppUserManager.CreateAsync(user, registerModel.Password);
                IdentityResult identityRoleResult = await AppUserManager.AddToRoleAsync(user.Id, "User");
                if(identityUserResult.Succeeded && identityRoleResult.Succeeded)
                {
                    return RedirectToAction("MainPage", "Home");
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Некорректный запрос");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel loginModel, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                User user = await AppUserManager.FindAsync(loginModel.UserName, loginModel.Password);
                if(user == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Страница не найдена");
                }
                else
                {
                    ClaimsIdentity claimsIdentity = await AppUserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claimsIdentity);
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("MainPage", "Home");
                    }
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(loginModel);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if(ModelState.IsValid)
            {
                User user = await AppUserManager.FindByEmailAsync(forgotPasswordModel.Email);
                if (user == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Некорректный запрос");
                }
                var provider = new DpapiDataProtectionProvider(user.UserName);
                AppUserManager.UserTokenProvider = new DataProtectorTokenProvider<User>(provider.Create(user.UserName));
                var code = await AppUserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account",
                    new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await AppUserManager.SendEmailAsync(user.Id, "Сброс пароля",
                    "Для сброса пароля, перейдите по ссылке <a href=\"" + callbackUrl + "\">сбросить</a>");
                return RedirectToAction("MainPage", "Home");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Некорректный запрос");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            if(ModelState.IsValid)
            {
                User user = await AppUserManager.FindByEmailAsync(resetPasswordModel.Email);
                if(user == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Некорректный запрос");
                }
                var provider = new DpapiDataProtectionProvider(user.UserName);
                AppUserManager.UserTokenProvider = new DataProtectorTokenProvider<User>(provider.Create(user.UserName));
                IdentityResult result = await AppUserManager.ResetPasswordAsync(user.Id, resetPasswordModel.Code, resetPasswordModel.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction("MainPage", "Home");
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Некорректный запрос");
        }
        [Authorize]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("MainPage", "Home");
        }
        public string GetPasswordHash(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
    }
}
