using bLibrary.DBContext;
using bLibrary.Models;
using PagedList;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace bLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly BLibraryContext bLibraryContext = BLibraryContext.CreateContext();
        const int pageSize = 20;
        [HttpGet]
        public async Task<ActionResult> MainPage(int? page)
        {
            int pageNumber = page ?? 1;
            IPagedList<Book> books = await Task.Run(() => bLibraryContext.Books.OrderByDescending(x => x.RecommendationsNum).ToPagedList(pageNumber, pageSize));
            return View(books);
        }
        [HttpPost]
        public async Task<ActionResult> Search(string name, int? page)
        {
            ViewBag.name = name;
            int pageNumber = page ?? 1;
            IPagedList<Book> books = await Task.Run(() => bLibraryContext.Books.OrderByDescending(x => x.RecommendationsNum).Where(x => x.Name.Contains(name)).ToPagedList(pageNumber, pageSize));
            return View(books);
        }
    }
}
//TODO: протестировать программу, рефакторинг,