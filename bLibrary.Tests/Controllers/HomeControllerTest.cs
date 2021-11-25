using bLibrary.DBContext;
using bLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace bLibrary.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void MainPage()
        {
            //List<Book> result;
            //using(BLibraryContext bLibraryContext = BLibraryContext.CreateContext())
            //{
            //    result = bLibraryContext.Books.OrderBy(x => x.Name).Take(100).ToList();
            //}
            //Assert.IsNotNull(result);
        }
    }
}
