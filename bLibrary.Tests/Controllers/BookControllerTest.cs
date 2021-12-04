using bLibrary.Controllers;
using bLibrary.DBContext;
using bLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace bLibrary.Tests.Controllers
{
    [TestClass]
    public class BookControllerTest
    {
        private readonly BLibraryContext db = BLibraryContext.CreateContext();
        [TestMethod]
        [DataRow(2)]
        public async Task GetBook(int value)
        {
            BookController bookController = new BookController();
            ViewResult viewResult = await bookController.GetBook(value) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        public async Task GetCreateBook()
        {
            BookController bookController = new BookController();
            ViewResult viewResult = await bookController.GetCreateBook() as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        public async Task CreateBook()
        {
            Book book = new Book
            {
                Author = "Достоевский Федор Михайлович",
                Name = "Преступление и наказание",
                Language = Language.Русский,
                Part = 1,
                Pages = 29,
                Description = "оциально-психологический и социально-философский роман Фёдора Михайловича Достоевского, над которым писатель работал в 1865—1866 годах.",
                Genre = await Task.Run(() => db.Genres.FirstOrDefault()),
                BookLink = "dostoyevsky.pdf",
                CoverLink = "dostoyevsky.jpg"
            };
            BookController bookController = new BookController();
            ViewResult viewResult = await bookController.CreateBook(book) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        [DataRow(2)]
        public async Task GetEditBook(int value)
        {
            BookController bookController = new BookController();
            ViewResult viewResult = await bookController.GetEditBook(value) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        public async Task EditBook()
        {
            Book book = new Book
            {
                Author = "Достоевский Федор Михайлович",
                Name = "Преступление и наказание",
                Language = Language.Русский,
                Part = 1,
                Pages = 29,
                Description = "Социально-психологический и социально-философский роман Фёдора Михайловича Достоевского, над которым писатель работал в 1865—1866 годах.",
                Genre = await Task.Run(() => db.Genres.FirstOrDefault()),
                BookLink = "dostoyevsky.pdf",
                CoverLink = "dostoyevsky.jpg"
            };
            BookController bookController = new BookController();
            ViewResult viewResult = await bookController.EditBook(book) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
        [TestMethod]
        [DataRow(2)]
        public async Task DeleteBook(int value)
        {
            BookController bookController = new BookController();
            ViewResult viewResult = await bookController.DeleteBook(value) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}
