using bLibrary.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bLibrary.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult MainPage()
        {
            //var i = BLibraryContext.CreateContext().Books.Where(x => x.Name == "123");
            return View();
        }
    }
}