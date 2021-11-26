using bLibrary.DBContext;
using bLibrary.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace bLibrary.Controllers
{
    public class BookController : Controller
    {
        private readonly BLibraryContext bLibraryContext = BLibraryContext.CreateContext();
        [HttpGet]
        public ActionResult GetBook(int id)
        
        {
            Book book = bLibraryContext.Books.Include(x => x.Genre).Include(x => x.Reviews).Where(x => x.BookId == id).FirstOrDefault();
            return View(book);
        }
    }
}