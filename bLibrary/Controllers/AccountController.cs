using bLibrary.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace bLibrary.Controllers
{
    public class AccountController : Controller
    {
        private AppUserManager UserManager
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
                    Nickname = registerModel.Nickname,
                    Email = registerModel.Email,
                    Age = registerModel.Age,
                };
                IdentityResult identityResult = await UserManager.CreateAsync(user, registerModel.Password);
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
                User user = await UserManager.FindAsync(loginModel.Email, loginModel.Password);
                if(user == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Страница не найдена");
                }
                else
                {
                    ClaimsIdentity claimsIdentity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
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
    }
}
