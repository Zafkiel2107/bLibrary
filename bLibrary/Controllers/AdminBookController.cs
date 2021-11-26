using bLibrary.DBContext;
using bLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace bLibrary.Controllers
{
    public class AdminBookController : Controller
    {
        private readonly BLibraryContext bLibraryContext = BLibraryContext.CreateContext();
        [HttpGet]
        public ActionResult GetViewCreateBook()
        {
            ViewBag.GenresList = bLibraryContext.Genres.Select(x => x.GenreName).OrderBy(x => x);
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateBook(Book book)
        {
            book.Genre = bLibraryContext.Genres.Where(x => x.GenreName == book.Genre.GenreName).FirstOrDefault();
            try
            {
                if(!book.BookLink.Contains("pdf") && !book.CoverLink.Contains("jpg"))
                {
                    throw new Exception("Неверный формат файла");
                }
                bLibraryContext.Books.Add(book);
                await bLibraryContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(422, ex.Message);
            }
            return RedirectToAction("MainPage", "Home");
        }
    }
}