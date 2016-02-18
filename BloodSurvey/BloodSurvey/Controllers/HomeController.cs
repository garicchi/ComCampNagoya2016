using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodSurvey.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult Apologize()
        {
            return View();
        }
        
        public ActionResult Evaluate()
        {
            return View();
        }
    }
}
