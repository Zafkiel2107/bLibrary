using bLibrary.DBContext;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace bLibrary.Controllers
{
    public class RoleController : Controller
    {
        private readonly BLibraryContext bLibraryContext = BLibraryContext.CreateContext();
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetEditRole(string id)
        {
            if(id != null)
            {
                IdentityUser identityUser = await Task.Run(() => bLibraryContext.Users.Find(id));
                if (identityUser != null)
                {
                    IdentityUserRole identityUserRole = identityUser.Roles.FirstOrDefault();
                    ViewBag.RoleList = await Task.Run(() => bLibraryContext.Roles.Select(x => x.Name));
                    return View(identityUserRole);
                }
                else
                    return HttpNotFound();
            }
            else
                return HttpNotFound();
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditRole(IdentityUserRole identityUserRole)
        {
            identityUserRole.RoleId = await Task.Run(() => bLibraryContext.Roles.Where(x => x.Name == identityUserRole.RoleId).Select(x => x.Id).FirstOrDefault());
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = await Task.Run(() => bLibraryContext.Users.Find(identityUserRole.UserId));
                if (identityUser != null)
                {
                    identityUser.Roles.Clear();
                    identityUser.Roles.Add(identityUserRole);
                    bLibraryContext.Entry(identityUser).State = EntityState.Modified;
                    await bLibraryContext.SaveChangesAsync();
                    return RedirectToAction("AdminPanel", "Layout");
                }
                else
                    return HttpNotFound();
            }
            else
                return new HttpStatusCodeResult(422, "Необрабатываемая сущность");
        }
    }
}