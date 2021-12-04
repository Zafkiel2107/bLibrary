using bLibrary.DBContext;
using bLibrary.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using System;
using System.Net;

namespace bLibrary.Controllers
{
    public class BookController : Controller
    {
        private readonly BLibraryContext bLibraryContext = BLibraryContext.CreateContext();
        [HttpGet]
        public async Task<ActionResult> GetBook(int id)
        {
            Book book = await Task.Run(() => bLibraryContext.Books.Include(x => x.Genre).Include(x => x.Reviews).Where(x => x.BookId == id).FirstOrDefault());
            return View(book);
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
            try
            {
                if (!book.BookLink.Contains("pdf") && !book.CoverLink.Contains("jpg"))
                {
                    throw new Exception("Неверный формат файла");
                }
                bLibraryContext.Books.Add(book);
                await bLibraryContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(422, ex.Message);
            }
            return RedirectToAction("MainPage", "Home");
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetEditBook(int id)
        {
            Book book = await bLibraryContext.Books.FindAsync(id);
            ViewBag.GenresList = await Task.Run(() => bLibraryContext.Genres.Select(x => x.GenreName).OrderBy(x => x));
            return View(book);
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult> EditBook(Book book)
        {
            book.Genre = await Task.Run(() => bLibraryContext.Genres.Where(x => x.GenreName == book.Genre.GenreName).FirstOrDefault()); //не работает
            bLibraryContext.Entry(book).State = EntityState.Modified;
            await bLibraryContext.SaveChangesAsync();
            return RedirectToAction("AdminPanel", "Layout");
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            Book book = await bLibraryContext.Books.FindAsync(id);
            bLibraryContext.Books.Remove(book);
            await bLibraryContext.SaveChangesAsync();
            return RedirectToAction("AdminPanel", "Layout");
        }
    }
}