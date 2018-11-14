using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using StaticResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Create()
        {
            var model = new Category();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            if(ModelState.IsValid)
            {
                string currentCulture = (string)Session[CommonConstants.CurrentCulture];
                model.Language = currentCulture;
                var id = new CategoryDao().Insert(model);
                if(id > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", Resources.InsertCategoryFailed);
                }
            }
            return View(model);
        }
    }
}