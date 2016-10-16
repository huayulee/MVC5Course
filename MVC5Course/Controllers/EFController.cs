using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: EF
        public ActionResult Index()
        {
            var data = this.db.Product.Where(x=>x.ProductName.StartsWith("White"));
            return View(data);
        }

        public ActionResult Create()
        {
            Product product = new Product()
            {
                ProductName = "White Treeee",
                Price = 299,
                Stock = 9,
                Active = true
            };

            this.db.Product.Add(product);
            this.db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            var product = db.Product.Find(id);
            return View(product);
        }

        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);
            this.db.Product.Remove(product);
            this.db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}