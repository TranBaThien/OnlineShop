using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(long cateId)
        {
                      
            var productdao = new ProductDao();          
            var model = productdao.FindbycateID(cateId);
            ViewBag.ProductCategory = new ProductCategoryDao().ViewDetail(cateId);

            return View(model);
        } 
        
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword)
        {
            var model = new ProductDao().search(keyword);
            ViewBag.Keyword = keyword;
            return View(model);
        }

        [OutputCache(CacheProfile = "Cache1DayForProduct")]
        public ActionResult Detail (long id)
        {
            var productdao = new ProductDao();         
            var model = productdao.FindbyproductID(id);
            ViewBag.RelatedProducts = productdao.ListRelatedProducts(id);
            ViewBag.ProductCategory = new ProductCategoryDao().ListAll();
            return View(model);
        }
    }
}