using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using System.Data.SqlClient;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {

        static List<Cart> carts = new List<Cart>();

        public ActionResult ViewCart(string userName, string sessionId)
        {
            userName = "johntan";

            carts = CartFunctions.CartList(userName);
            ViewData["username"] = userName;
            ViewData["sessionId"] = sessionId;
            return View(carts);
        }
        
        public ActionResult CheckOut(string userName, string sessionId)
        {

            foreach (string key in Request.Form.AllKeys)
            {
                var value = Request[key];
                int quantity = Convert.ToInt32(value);
                int productId = Convert.ToInt32(key);
                Cart cart = carts.Find(c => c.ProductId == productId);
                for (int i = 0; i < quantity; i++)
                {
                    CheckoutFunctions.ItemPurchase(cart);
                }
            }
            ViewData["username"] = userName;
            ViewData["sessionId"] = sessionId;
            return RedirectToAction("ViewPurchases", "History", new { username = userName, sessionid = sessionId });
        }

        public ActionResult ContinueShopping(string userName, string sessionId)
        {
            return RedirectToAction("ViewProducts", "Gallery", new { username = userName, sessionid = sessionId });
        }

        //[HttpPost]
        //public JsonResult TotalPrice(string productId, string quantity)
        //{
        //    double total = 0;
        //    int pId = Convert.ToInt32(productId);
        //    int qty = Convert.ToInt32(quantity);

        //    foreach (var cart in carts)
        //    {
        //        if(cart.ProductId == Convert.ToInt32(pId))
        //        {
        //            cart.Quantity = qty;
        //            total += qty * cart.Product.Price;
        //        }
        //        else
        //        {
        //            total += cart.Quantity * cart.Product.Price;
        //        }
        //    }
        //    return Json(new { d = total });
        //}
    }
}