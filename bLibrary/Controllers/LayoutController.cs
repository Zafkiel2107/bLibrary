using bLibrary.DBContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace bLibrary.Controllers
{
    public class LayoutController : Controller
    {
        private readonly BLibraryContext bLibraryContext = BLibraryContext.CreateContext();
        [HttpGet, Authorize] //(Roles = "Admin")]
        public ActionResult AdminPanel()
        {
            ViewBag.Books = bLibraryContext.Books.Include(x => x.Genre);
            ViewBag.Roles = bLibraryContext.Users.Include(x => x.Roles);
            return View();
        }
        [HttpGet, Authorize]
        public ActionResult Settings()
        {
            IdentityUser user = bLibraryContext.Users.Find(User.Identity.GetUserId());
            return View(user);
        }
        [HttpPost, Authorize]
        public async Task<ActionResult> EditSettings(IdentityUser identityUser)
        {
            bLibraryContext.Entry(identityUser).State = EntityState.Modified;
            await bLibraryContext.SaveChangesAsync();
            return RedirectToAction("MainPage", "Home");
        }
    }
}
