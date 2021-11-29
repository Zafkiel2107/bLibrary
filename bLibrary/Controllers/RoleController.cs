using bLibrary.DBContext;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Data.Entity;

namespace bLibrary.Controllers
{
    public class RoleController : Controller
    {
        private readonly BLibraryContext bLibraryContext = BLibraryContext.CreateContext();
        [HttpGet, Authorize(Roles = "Admin")]
        public ActionResult GetEditRole(string id)
        {
            IdentityUserRole identityUserRole = bLibraryContext.Users.Find(id).Roles.FirstOrDefault();
            ViewBag.RoleList = bLibraryContext.Roles.Select(x => x.Name);
            return View(identityUserRole);
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditRole(IdentityUserRole identityUserRole)
        {
            identityUserRole.RoleId = bLibraryContext.Roles.Where(x => x.Name == identityUserRole.RoleId).Select(x => x.Id).FirstOrDefault();
            IdentityUser identityUser = bLibraryContext.Users.Find(identityUserRole.UserId);
            identityUser.Roles.Clear();
            identityUser.Roles.Add(identityUserRole);
            bLibraryContext.Entry(identityUser).State = EntityState.Modified;
            await bLibraryContext.SaveChangesAsync();
            return RedirectToAction("AdminPanel", "Layout");
        }
    }
}