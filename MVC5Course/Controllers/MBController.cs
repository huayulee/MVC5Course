using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    ////[HandleError(ExceptionType = typeof(Exception), View = "Error_CommonException")]
    [HandleError(ExceptionType =typeof(DbEntityValidationException),View = "Error_InvalidOperationException")]
    public class MBController : BaseController
    {
        private ProductRepository repo = RepositoryHelper.GetProductRepository();

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

        public ActionResult ProductList()
        {
            ViewBag.errMsg = TempData["ErrorMessage"];
            var data = this.repo.Get所有產品_依據ProductId大到小排序(10);
            return View(data);
        }

        [HttpPost]
        public ActionResult BatchUpdate(IList<ProductBatchUpdateViewModel> items)
        {
            if (ModelState.IsValid) //<--這裡會驗證ViewModel驗證
            {
                foreach (var item in items)
                {
                    var product = repo.Find(item.ProductId);
                    product.ProductName = item.ProductName;
                    product.Price = item.Price;
                    product.Active = item.Active;
                    product.Stock = item.Stock;
                }

                this.repo.UnitOfWork.Commit(); // <-- 這裡會驗證Model驗證
            }
            else
            {
                foreach(var a in ModelState.Values)
                {
                    foreach(var b in a.Errors)
                    {
                        string bb = b.ErrorMessage;
                        TempData["ErrorMessage"] = b.ErrorMessage;
                    }
                }
            }

            return RedirectToAction("ProductList");
        }

        public ActionResult ErrorHandler()
        {
            throw new InvalidOperationException("ERRRRROR");
            return View();
        }
    }
}