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
            return View();
        }
    }
}