using bLibrary.DBContext;
using bLibrary.Models;
using bLibrary.Models.Identity;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace bLibrary.Controllers
{
    public class ReviewController : Controller
    {
        private readonly BLibraryContext bLibraryContext = BLibraryContext.CreateContext();
        [HttpGet, Authorize]
        public async Task<ActionResult> GetCreateEditReview(int? id)
        {
            if (id != null)
            {
                Review review = await Task.Run(() => bLibraryContext.Reviews.Include(x => x.Book).Include(x => x.User).Where(x => x.Book.BookId == id).Where(x => x.User.UserName == User.Identity.Name).FirstOrDefault());
                if (review == null)
                {
                    review = CreateReviewDraft(id);
                    return View("~/Views/Review/GetCreateReview.cshtml", review);
                }
                else
                {
                    return View("~/Views/Review/GetEditReview.cshtml", review);
                }
            }
            else
                return HttpNotFound();
        }
        [HttpPost, Authorize]
        public async Task<ActionResult> CreateReview(Review review)
        {
            review.Book = await bLibraryContext.Books.FindAsync(review.Book.BookId);
            review.User = await Task.Run(() => bLibraryContext.Users.Where(x => x.UserName == review.User.UserName).FirstOrDefault()) as User;
            if(ModelState.IsValid)
            {
                bLibraryContext.Entry(review).State = EntityState.Added;
                await bLibraryContext.SaveChangesAsync();
                return RedirectToAction("GetBook", "Book", new { id = review.Book.BookId });
            }
            else
                return new HttpStatusCodeResult(422, "Необрабатываемая сущность");
        }
        [HttpPost, Authorize]
        public async Task<ActionResult> EditReview(Review review)
        {
            review.Book = await bLibraryContext.Books.FindAsync(review.Book.BookId);
            review.User = await Task.Run(() => bLibraryContext.Users.Where(x => x.UserName == review.User.UserName).FirstOrDefault()) as User;
            if(ModelState.IsValid)
            {
                bLibraryContext.Entry(review).State = EntityState.Modified;
                await bLibraryContext.SaveChangesAsync();
                return RedirectToAction("GetBook", "Book", new { id = review.Book.BookId });
            }
            else
                return new HttpStatusCodeResult(422, "Необрабатываемая сущность");
        }
        [HttpGet, Authorize]
        public async Task<ActionResult> DeleteReview(int? id)
        {
            if(id != null)
            {
                Review review = await bLibraryContext.Reviews.FindAsync(id);
                if (review != null)
                {
                    bLibraryContext.Entry(review).State = EntityState.Deleted;
                    await bLibraryContext.SaveChangesAsync();
                    return RedirectToAction("MainPage", "Home");
                }
                else
                    return HttpNotFound();
            }
            else
                return HttpNotFound();
        }
        private Review CreateReviewDraft(int? id)
        {
            return new Review
            {
                Book = new Book
                {
                    BookId = id.Value
                },
                User = new User
                {
                    UserName = User.Identity.Name
                }
            };
        }
    }
}