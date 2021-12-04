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
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult> AdminPanel()
        {
            ViewBag.Books = await Task.Run(() => bLibraryContext.Books);
            ViewBag.Users = await Task.Run(() => bLibraryContext.Users); 
            return View();
        }
        [HttpGet, Authorize]
        public async Task<ActionResult> Settings()
        {
            IdentityUser user = await Task.Run(() => bLibraryContext.Users.Find(User.Identity.GetUserId()));
            return View(user);
        }
        [HttpPost, Authorize]
        public async Task<ActionResult> EditSettings(IdentityUser identityUser)
        {
            bLibraryContext.Entry(identityUser).State = EntityState.Modified;
            await bLibraryContext.SaveChangesAsync();
            return RedirectToAction("AdminPanel", "Layout");
        }
    }
}
