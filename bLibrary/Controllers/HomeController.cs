using bLibrary.DBContext;
using bLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace bLibrary.Controllers
{
    public class HomeController : Controller
    {
        private readonly BLibraryContext bLibraryContext = BLibraryContext.CreateContext();
        [HttpGet]
        public ActionResult MainPage()
        {
            IEnumerable<Book> books = bLibraryContext.Books.OrderByDescending(x => x.RecommendationsNum).Take(20);
            ViewBag.Title = "Популярные произведения";
            return View(books);
        }
        [HttpPost]
        public ActionResult Search(string name)
        {
            IEnumerable<Book> books = bLibraryContext.Books.Where(x => x.Name.Contains(name));
            ViewBag.Title = $"Найдено результатов : {books.Count()}";
            return View("/Views/Home/MainPage.cshtml", books);
        }
    }
}
//TODO: ТЕСТЫ, АСИНХРОННЫЕ МЕТОДЫ К БД