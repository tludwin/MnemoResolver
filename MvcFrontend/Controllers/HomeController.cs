using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFrontend.Controllers
{
    public class HomeController : Controller
    {
        readonly string _dictionaryPath = @"~\Content\dictionary";

        public ActionResult Index(string text)
        {
            ViewBag.SearchText = text;
            ViewBag.DictionaryPath = Server.MapPath(_dictionaryPath);

            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            ViewBag.SearchText = collection["SearchBox"];
            ViewBag.DictionaryPath = Server.MapPath(_dictionaryPath);
            return View();
        }
    }
}
