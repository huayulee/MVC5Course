using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : BaseController
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult PartialViewTest()
        {
            return PartialView();
        }

        public ActionResult ContentTest()
        {
            return Content("ASP.NET 開發實戰", "text/plan", Encoding.Unicode);
        }

        public ActionResult FileTest()
        {
            //C:\Users\1406014\Source\Repos\MVC5Course\MVC5Course\Content\FileTest01.jpg
            string filePath = Server.MapPath(@"~/Content/FileTest01.jpg");
            return File(filePath, "image/jpeg");
        }

        public ActionResult FileTest2()
        {
            string filePath = Server.MapPath(@"~/Content/FileTest01.jpg");
            return File(filePath, "image/jpeg", "FileIce.jpg");
        }

        public ActionResult JsonTest()
        {
            this.db.Configuration.LazyLoadingEnabled = false;
            var data = this.db.Product.Take(10);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}