using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ShoppingCart.Models
{
    //Handles queries pertaining to past purchases
    public class PurchasesData
    {
        public static List<Transaction> GetPurchasesToBeDisplayed(string userName, DateTime dateTime,SqlConnection conn)
        {
            List<Transaction> rowsInpurchasePage = new List<Transaction>();

            string q1 = @"SELECT ProductID, Date, COUNT(ProductID) as Qty from Purchase where Username='" + userName + "'" + "group by ProductID,Date ORDER by DATE desc, ProductID";
            SqlCommand cmd1 = new SqlCommand(q1, conn);

            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                Transaction rowInpurchasePage = new Transaction()
                {
                    ProductId = (int)reader["ProductID"],
                    PurchaseDate = dateTime.AddSeconds((long)reader["Date"]),
                    Quantity = (int)reader["Qty"]
                };
                rowsInpurchasePage.Add(rowInpurchasePage);
            }
            reader.Close();
            return rowsInpurchasePage;
        }

        public static List<PurchaseDetails> GetPurchasesDetailsByLineItems(string userName,DateTime dateTime, SqlConnection conn)
        {
            List<PurchaseDetails> purchasedItems = new List<PurchaseDetails>();

            string q1 = @"SELECT AtvCode,Purchase.ProductID,ProductName,Description,Date from Product,Purchase where Product.ProductID = Purchase.ProductID and Username = '" + userName + "'order by date desc, ProductID";
            SqlCommand cmd1 = new SqlCommand(q1, conn);

            SqlDataReader reader = cmd1.ExecuteReader();
            while (reader.Read())
            {
                PurchaseDetails purchasedItem = new PurchaseDetails()
                {
                    AtvCode = (string)reader["AtvCode"],
                    ProductId = (int)reader["ProductId"],
                    ProductName = (string)reader["ProductName"],
                    Description = (string)reader["Description"],
                    PurchaseDate = dateTime.AddSeconds((long)reader["Date"])
                };
                purchasedItems.Add(purchasedItem);
            }
            reader.Close();
            return purchasedItems;
        }
    }
}