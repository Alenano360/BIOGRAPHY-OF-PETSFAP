using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BIOGRAPHY_OF_PETSFAP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["HiddenFieldRol"] = Session["RolUsuarioSession"];
            return View();
        }
    }
}