using Model.Dao;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart

        private List<CartItem> GetCart()
        {
            List<CartItem> cart = (List<CartItem>)Session[CartSession];
            if(cart == null)
            {
                cart = new List<CartItem>();
                Session[CartSession] = cart;
            }
            return cart;
        }

        public ActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        public JsonResult DeleteAll()
        {
            var cart = GetCart();
            cart.Clear();
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var cart = GetCart();
            var product = new ProductDao();
            var sp = product.FindbyproductID(id);
            if(sp != null)
            {
                cart.RemoveAll(x => x.Product.ID == id);
            }
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Update(string cartModel)
        {
            var cart = GetCart();
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);

            foreach(var item in cart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if(jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }

            return Json(new
            {
                status = true
            });
        }

        public ActionResult AddItem(long productId, int quantity)
        {
            var cart = GetCart();
            var product = new ProductDao();
            var sp = product.FindbyproductID(productId);
            if(sp != null)
            {
                var line = cart.FirstOrDefault(x => x.Product.ID == productId);
                if(line == null)
                {
                    cart.Add(new CartItem()
                    {
                        Product = sp,
                        Quantity = quantity
                    });
                }
                else
                {
                    line.Quantity += quantity;
                }
            }
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = GetCart();
            ViewBag.TotalPrice = cart.Sum(x => (x.Product.Price * x.Quantity));
            return PartialView(cart);
        }


        public ActionResult Payment()
        {
            return View();
        }
    }
}