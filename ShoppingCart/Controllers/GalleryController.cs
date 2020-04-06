using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using System.Data.SqlClient;

namespace ShoppingCart.Controllers
{
    public class GalleryController : Controller
    {
        // GET: View
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ViewProducts(string userName, string sessionId,string input)
        {
            List<Product> products = new List<Product>();
            ViewData["products"] = ProductData.GetProductDetails(input);

            using (SqlConnection conn = new SqlConnection(Data.db_cfg))
            {
                conn.Open();
                //Keep track of total number of items added
                string q = @"SELECT SUM(Quantity) from Cart where UserName = '" + userName + "'";
                SqlCommand cmd = new SqlCommand(q, conn);
                ViewData["itemcount"] = cmd.ExecuteScalar();
            }
            ViewData["username"] = userName;
            ViewData["sessionId"] = sessionId;
            ViewData["firstandlastnames"] = UserData.GetUserFirstandLastNames(userName);
            return View();
        }
        
        public ActionResult AddtoCart(int productId, string userName,string sessionId)
        {
            //Method to call whenever user clicks "Add to Cart" under "Gallery" page/increases quantity under "My Purchases" page
            CartFunctions.AddtoCart(productId,userName);

            ViewData["sessionId"] = sessionId;
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult RemovefromCart(int productId, string userName)
        {
            //Method to call whenever user decreases quantity by 1 with each click under "My Purchases"
            //To ensure that the decrement button html has limit on how low it can go
            CartFunctions.RemovefromCart(productId, userName);
          
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult EmptyCart(int productId, string userName)
        {
            //Method to call whenever user has checked out items in cart
            CartFunctions.EmptyCart(userName);

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
