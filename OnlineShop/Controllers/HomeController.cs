using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var slides = new SlideDao().ListAll();            
            return View(slides);
        }

        [ChildActionOnly] 
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupId(1);
            return PartialView(model);
        }

        [ChildActionOnly]
        //[OutputCache(Duration = 3600 * 24)]
        public ActionResult TopMenu()
        {
            var model = new MenuDao().ListByGroupId(2);
            return PartialView(model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600 * 24)]
        public ActionResult Footer()
        {
            var model = new FooterDao().GetFooter();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult NewProduct()
        {
            var newproduct = new ProductDao();
            var model = newproduct.ListNewProduct(4);
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult FeatureProduct()
        {
            var featureproduct = new ProductDao();
            var model = featureproduct.ListFeatureProduct(4);
            return PartialView(model);
        }
    }
}