using bLibrary.DBContext;
using bLibrary.Models;
using bLibrary.Models.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace bLibrary.Controllers
{
    public class ReviewController : Controller
    {
        private readonly BLibraryContext bLibraryContext = BLibraryContext.CreateContext();
        private AppUserManager AppUserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }
        [HttpGet, Authorize]
        public async Task<ActionResult> GetCreateReview(int id)
        {
            Review review = new Review
            {
                Book = await bLibraryContext.Books.FindAsync(id),
                User = await AppUserManager.FindByNameAsync(User.Identity.Name)
            };
            return View(review);
        }
        [HttpPost, Authorize]
        public async Task<ActionResult> CreateReview(Review review)
        {
            review.Book = await bLibraryContext.Books.FindAsync(review.Book.BookId);
            review.User = await AppUserManager.FindByIdAsync(review.User.Id);
            bLibraryContext.Reviews.Add(review);
            await bLibraryContext.SaveChangesAsync();
            return RedirectToAction("GetBook", "Book", new { review.Book.BookId });
        }
        [HttpGet, Authorize]
        public ActionResult GetEditReview(int id)
        {
            return View();
        }
        [HttpPost, Authorize]
        public async Task<ActionResult> EditReview(Review review)
        {
            return View();
        }
        [HttpPost, Authorize]
        public async Task<ActionResult> DeleteReview(int id)
        {
            return View();
        }
    }
}