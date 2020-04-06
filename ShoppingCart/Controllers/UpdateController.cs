using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class UpdateController : Controller
    {
        public ActionResult ViewCart(string userName, string sessionId)
        {
            List<Cart> carts = new List<Cart>();

            using (SqlConnection conn = new SqlConnection(Data.db_cfg))
            {
                conn.Open();

                string q1 = @"SELECT Username, p.ProductID as ProductID, Quantity, " +
                            "ProductName, Description, Price from Cart c, Product p where c.Username = '" + userName +
                            "' and c.ProductID = p.ProductID";
                SqlCommand cmd = new SqlCommand(q1, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = (int)reader["ProductID"],
                        Description = (string)reader["Description"],
                        Price = Convert.ToDouble(reader["Price"]),
                        ProductName = (string)reader["ProductName"]
                    };

                    Cart cart = new Cart()
                    {
                        ProductId = (int)reader["ProductID"],
                        UserName = (string)reader["Username"],
                        Quantity = (int)reader["Quantity"],
                        Product = product

                    };
                    carts.Add(cart);
                }
                reader.Close();
            }
            ViewData["username"] = userName;
            ViewData["Cart"] = carts;
            ViewData["firstandlastnames"] = UserData.GetUserFirstandLastNames(userName);
            return View();
        }

        public ActionResult AddtoCart(int productId, string userName, string sessionId)
        {
            //Method to call whenever user clicks "Add to Cart" under "Gallery" page/increases quantity under "My Purchases" page
            CartFunctions.AddtoCart(productId, userName);

            ViewData["sessionId"] = sessionId;
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult UpdateCart(int productId, string userName, int qty)
        {
            //Method to call whenever user decreases quantity by 1 with each click under "My Purchases"
            //To ensure that the decrement button html has limit on how low it can go
            CartFunctions.UpdateCart(productId, userName, qty);

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

        public ActionResult Checkout(string userName, string sessionId)
        {
            DateTime refTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime currentDate = DateTime.Now;

            long unixTime = (long)(currentDate - refTime).TotalSeconds;
            unixTime = (long)(currentDate - refTime).TotalSeconds;

            //Update purchase table
            //CheckoutFunctions.UpdatePurchaseTable(userName, unixTime);

            //Clear cart after check out
            CartFunctions.EmptyCart(userName);
            return RedirectToAction("ViewPurchases", "History");
        }
    }
}