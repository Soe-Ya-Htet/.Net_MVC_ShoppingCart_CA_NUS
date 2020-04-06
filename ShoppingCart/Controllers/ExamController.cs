using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class ExamController : Controller
    {
        // GET: Exam
        [Route("Form")]  //routes.MapMvcAttributeRoutes() at RouteConfig.cs
        public ActionResult ExamForm()
        {

            return View();
        }
    }
}