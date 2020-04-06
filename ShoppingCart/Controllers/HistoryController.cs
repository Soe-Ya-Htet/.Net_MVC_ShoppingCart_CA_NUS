using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using ShoppingCart.Filters;

namespace ShoppingCart.Controllers
{
    public class HistoryController : Controller
    {
        // GET: History

        [PersonalPurchasesFilter]
        public ActionResult ViewPurchases(string userName, string sessionId)
        {
            // Format our DateTime object to start at the UNIX Epoch
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            
            using (SqlConnection conn = new SqlConnection(Data.db_cfg))
            {
                conn.Open();

                //Group by product with multiple quantities bought on same day ordered by day
                List<Transaction> rowsInpurchasePage = PurchasesData.GetPurchasesToBeDisplayed(userName, dateTime,conn);
                
                //All products and its details (activation codes,productid,name,description) purchased ordered by date
                List<PurchaseDetails> purchasedItems = PurchasesData.GetPurchasesDetailsByLineItems(userName, dateTime,conn);

                ViewData["purchases"] = rowsInpurchasePage;
                ViewData["purchaseditems"] = purchasedItems;
                ViewData["sessionId"] = sessionId;
            }
            return View();
        }
    }
}