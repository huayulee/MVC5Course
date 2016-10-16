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
    public class EFController : Controller
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: EF
        public ActionResult Index()
        {
            var data = this.db.Product.Where(x=>x.ProductName.StartsWith("White"));
            return View(data);
        }

        public ActionResult Create(decimal? price )
        {
            Product product = new Product()
            {
                ProductName = "White Treeee",
                Price = price,
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
            this.db.OrderLine.RemoveRange(product.OrderLine);
            this.db.Product.Remove(product);
            
            this.db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            var product = this.db.Product.Find(id);
            product.ProductName += "!";

            try
            {
                this.db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult resultError in ex.EntityValidationErrors)
                {
                    foreach (var vErrors in resultError.ValidationErrors)
                    {
                        throw new DbEntityValidationException(string.Format("{0}發生錯誤。{1}", vErrors.PropertyName, vErrors.ErrorMessage));
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Price20PercentUp()
        {
            // 方法1.
            ////var products = this.db.Product.Where(x=>x.ProductName.StartsWith("White"));

            ////foreach (var item in products)
            ////{
            ////    item.Price = item.Price * 1.2m;
            ////}

            ////this.db.SaveChanges();

            // 方法2. 直接下T-SQL更新資料
            string keyword = "%White%";
            this.db.Database.ExecuteSqlCommand("UPDATE Product SET Price = Price * 1.2 WHERE ProductName LIKE @p0", keyword);

            return RedirectToAction("Index");
        }

        public ActionResult ClientContribution()
        {
            // 方法1.
            var data = this.db.vw_ClientContribution.Take(20);

            return View(data);
        }

        public ActionResult ClientContributionBySQL(string keyword = "%Mary%")
        {
            // 方法2. 直接下T-SQL查詢資料
            var data = this.db.Database.SqlQuery<ClientContributioViewModel>("SELECT c.ClientId,c.FirstName, c.LastName,(SELECT ISNULL(SUM(o.OrderTotal),0) FROM [dbo].[Order] o WHERE o.ClientId = c.ClientId) as OrderTotal FROM [dbo].[Client] as c WHERE c.FirstName LIKE @p0 ", keyword);

            return View(data);
        }

        public ActionResult ClientContribution3(string keyword = "%Lisa%")
        {
            // 使用Stored Procedure查詢
            return View(this.db.usp_GetClientContribution("%" + keyword + "%"));
        }
    }
}