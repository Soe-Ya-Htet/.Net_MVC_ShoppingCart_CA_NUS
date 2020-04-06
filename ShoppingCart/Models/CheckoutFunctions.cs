using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ShoppingCart.Models
{
    public class CheckoutFunctions
    {

        public static void ItemPurchase(Cart cart)
        {
            DateTime dateTime = DateTime.Now;
            DateTimeOffset dto = new DateTimeOffset(dateTime);
            string activationCode = Guid.NewGuid().ToString();
            using (SqlConnection conn = new SqlConnection(Data.db_cfg))
            {
                conn.Open();

                string deleteQuery = "DELETE FROM Cart where Username = 'johntan'";
                SqlCommand cmd1 = new SqlCommand(deleteQuery, conn);
                cmd1.ExecuteNonQuery();

                // Insert new purchase data with activation code
                string insertQuery = "INSERT INTO Purchase (AtvCode, ProductID, Username, Date)"
                    + $"VALUES ('{activationCode}',{cart.ProductId},'{cart.UserName}',{dto.ToUnixTimeSeconds()})";
                SqlCommand cmd2 = new SqlCommand(insertQuery, conn);
                cmd2.ExecuteNonQuery();

            }
        }

        //public static void UpdatePurchaseTable(string userName, long unixTime)
        //{
        //    List<Transaction> itemsTocheckOut = new List<Transaction>();

        //    using (SqlConnection conn = new SqlConnection(Data.db_cfg))
        //    {
        //        conn.Open();

        //        string q1 = @"SELECT ProductID, Quantity from Cart where Username='" + userName + "'";
        //        SqlCommand cmd1 = new SqlCommand(q1, conn);

        //        SqlDataReader reader = cmd1.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Transaction itemTocheckOut = new Transaction()
        //            {
        //                ProductId = (int)reader["ProductID"],
        //                Quantity = (int)reader["Quantity"]
        //            };
        //            itemsTocheckOut.Add(itemTocheckOut);
        //        }
        //        reader.Close();

        //        foreach (var t in itemsTocheckOut)
        //        {
        //            while (t.Quantity > 0)
        //            {
        //                string AtvCode = Guid.NewGuid().ToString();
        //                string q2 = "INSERT INTO Purchase (AtvCode,ProductID,Username,Date)" + "VALUES ('" + AtvCode + "','" + t.ProductId + "','" + userName + "','" + unixTime + "')";
        //                SqlCommand cmd2 = new SqlCommand(q2, conn);
        //                t.Quantity--;
        //                cmd2.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}
    }
}