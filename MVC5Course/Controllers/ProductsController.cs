using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class ProductsController : Controller
    {
        //private FabricsEntities db = new FabricsEntities();
        private ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
        [Route("Prod/List")]
        public ActionResult Index()
        {
            // 使用EntityFramework
            //return View(db.Product.Where(p => p.IsDeleted == false).OrderByDescending(p => p.ProductId).Take(10).ToList());

            // 改用Repository
            //ProductRepository repo = new ProductRepository();
            //repo.UnitOfWork = new EFUnitOfWork();
            //var data = this.repo.All().Where(p => p.IsDeleted == false).OrderBy(p => p.ProductId).Take(10).ToList();

            // 改用Override
            var data = this.repo.Get所有產品_依據ProductId大到小排序(10);

            return View(data);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // 使用EntityFramework
            // Product product = db.Product.Find(id);

            // 改用Repository
            var product = this.repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                // 使用EntityFramework
                //db.Product.Add(product);
                //db.SaveChanges();

                // 改用Repository
                this.repo.Add(product);
                this.repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // 使用EntityFramework
            //Product product = db.Product.Find(id);

            // 改用Repository
            var product = this.repo.Find(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                // 使用EntityFramework
                var db = this.repo.UnitOfWork.Context;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // 使用EntityFramework
            //Product product = db.Product.Find(id);

            // 改用Repository
            var product = this.repo.Find(id.Value);
            
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // 使用EntityFramework
            //Product product = db.Product.Find(id);
            // db.Product.Remove(product);
            //product.IsDeleted = true;
            //db.SaveChanges();

            // 改用Repository
            Product product = this.repo.Find(id);
            this.repo.Delete(product);  // 覆寫Delete

            // product.IsDeleted = true;
            this.repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                this.repo.UnitOfWork.Context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}