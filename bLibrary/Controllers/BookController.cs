using bLibrary.DBContext;
using bLibrary.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace bLibrary.Controllers
{
    public class BookController : Controller
    {
        private readonly BLibraryContext bLibraryContext = BLibraryContext.CreateContext();
        [HttpGet]
        public async Task<ActionResult> GetBook(int? id)
        {
            if(id != null)
            {
                Book book = await Task.Run(() => bLibraryContext.Books.Include(x => x.Genre).Include(x => x.Reviews).Where(x => x.BookId == id).FirstOrDefault());
                if (book != null)
                    return View(book);
                else
                    return HttpNotFound();
            }
            else
                return HttpNotFound();
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetCreateBook()
        {
            ViewBag.GenresList = await Task.Run(() => bLibraryContext.Genres.Select(x => x.GenreName).OrderBy(x => x));
            return View();
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateBook(Book book)
        {
            book.Genre = await Task.Run(() => bLibraryContext.Genres.Where(x => x.GenreName == book.Genre.GenreName).FirstOrDefault());
            if (ModelState.IsValid)
            {
                if (!book.BookLink.Contains("pdf") && !book.CoverLink.Contains("jpg"))
                {
                    return new HttpStatusCodeResult(422, "Неверный формат файла");
                }
                bLibraryContext.Books.Add(book);
                await bLibraryContext.SaveChangesAsync();
                return RedirectToAction("MainPage", "Home");
            }
            else
                return new HttpStatusCodeResult(422, "Необрабатываемая сущность");
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetEditBook(int? id)
        {
            if(id != null)
            {
                Book book = await bLibraryContext.Books.FindAsync(id);
                ViewBag.GenresList = await Task.Run(() => bLibraryContext.Genres.Select(x => x.GenreName).OrderBy(x => x));
                if (book != null)
                    return View(book);
                else
                    return HttpNotFound();
            }
            else
                return HttpNotFound();
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditBook(Book book)
        {
            book.Genre = await Task.Run(() => bLibraryContext.Genres.Where(x => x.GenreName == book.Genre.GenreName).FirstOrDefault()); //не работает
            if (ModelState.IsValid)
            {
                bLibraryContext.Entry(book).State = EntityState.Modified;
                await bLibraryContext.SaveChangesAsync();
                return RedirectToAction("AdminPanel", "Layout");
            }
            else
                return new HttpStatusCodeResult(422, "Необрабатываемая сущность");
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteBook(int? id)
        {
            if (id != null)
            {
                Book book = await bLibraryContext.Books.FindAsync(id);
                if (book != null)
                {
                    bLibraryContext.Books.Remove(book);
                    await bLibraryContext.SaveChangesAsync();
                    return RedirectToAction("AdminPanel", "Layout");
                }
                else
                    return HttpNotFound();
            }
            else
                return HttpNotFound();
        }
    }
}