using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    public class MBController : Controller
    {
        // GET: MB
        public ActionResult Index()
        {
            ViewData["Temp1"] = new LoginClientViewModel() { FirstName = "Jon Snow" };
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(LoginClientViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                TempData["MyData"] = viewModel;
                return RedirectToAction("ResultPage");
            }

            return View();
        }

        public ActionResult ResultPage()
        {
            ViewBag.myData = TempData["MyData"];
            return View();
        }
    }
}