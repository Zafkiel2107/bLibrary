using bLibrary.DBContext;
using bLibrary.Models;
using bLibrary.Models.Identity;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;

namespace bLibrary.Controllers
{
    public class ReviewController : Controller
    {
        private readonly BLibraryContext bLibraryContext = BLibraryContext.CreateContext();
        [HttpGet, Authorize]
        public async Task<ActionResult> GetCreateEditReview(int id)
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
        [HttpPost, Authorize]
        public async Task<ActionResult> CreateReview(Review review)
        {
            review.Book = await bLibraryContext.Books.FindAsync(review.Book.BookId);
            review.User = await Task.Run(() => bLibraryContext.Users.Where(x => x.UserName == review.User.UserName).FirstOrDefault()) as User;
            bLibraryContext.Entry(review).State = EntityState.Added;
            await bLibraryContext.SaveChangesAsync();
            return RedirectToAction("GetBook", "Book", new { id = review.Book.BookId });
        }
        [HttpPost, Authorize]
        public async Task<ActionResult> EditReview(Review review)
        {
            review.Book = await bLibraryContext.Books.FindAsync(review.Book.BookId);
            review.User = await Task.Run(() => bLibraryContext.Users.Where(x => x.UserName == review.User.UserName).FirstOrDefault()) as User;
            bLibraryContext.Entry(review).State = EntityState.Modified;
            await bLibraryContext.SaveChangesAsync();
            return RedirectToAction("GetBook", "Book", new { id = review.Book.BookId });
        }
        [HttpGet, Authorize]
        public async Task<ActionResult> DeleteReview(int id)
        {
            Review review = await bLibraryContext.Reviews.FindAsync(id);
            bLibraryContext.Entry(review).State = EntityState.Deleted;
            await bLibraryContext.SaveChangesAsync();
            return RedirectToAction("MainPage", "Home");
        }
        private Review CreateReviewDraft(int id)
        {
            return new Review
            {
                Book = new Book
                {
                    BookId = id
                },
                User = new User
                {
                    UserName = User.Identity.Name
                }
            };
        }
    }
}