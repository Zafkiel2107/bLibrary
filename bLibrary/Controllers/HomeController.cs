using bLibrary.DBContext;
using bLibrary.Models;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace bLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly BLibraryContext bLibraryContext = BLibraryContext.CreateContext();
        const int pageSize = 20;
        [HttpGet]
        public ActionResult MainPage(int? page)
        {
            int pageNumber = page ?? 1;
            IPagedList<Book> books = bLibraryContext.Books.OrderByDescending(x => x.RecommendationsNum).ToPagedList(pageNumber, pageSize);
            return View(books);
        }
        public ActionResult Search(string name, int? page)
        {
            ViewBag.name = name;
            int pageNumber = page ?? 1;
            IPagedList<Book> books = bLibraryContext.Books.OrderByDescending(x => x.RecommendationsNum).Where(x => x.Name.Contains(name)).ToPagedList(pageNumber, pageSize);
            return View(books);
        }
    }
}
//TODO: протестировать программу, рефакторинг, написать ТЕСТЫ, АСИНХРОННЫЕ МЕТОДЫ К БД