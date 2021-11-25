using bLibrary.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
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
                IdentityResult identityResult = await AppUserManager.CreateAsync(user, registerModel.Password);
                if(identityResult.Succeeded)
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
