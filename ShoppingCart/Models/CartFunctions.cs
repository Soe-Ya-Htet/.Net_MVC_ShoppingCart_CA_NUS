using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ShoppingCart.Models
{
    //Handles cart-related functions
    public class CartFunctions
    {
        public static void AddtoCart(int productId, string userName)
        {
            using (SqlConnection conn = new SqlConnection(Data.db_cfg))
            {
                conn.Open();
                int qty = 1;

                //To check if product exist in cart for the particular user
                string q1 = @"SELECT COUNT(*) from Cart where ProductID = '" + productId + "' and UserName = '" + userName + "'";
                SqlCommand cmd1 = new SqlCommand(q1, conn);
                int count = (int)cmd1.ExecuteScalar();

                //To update quantity if product has earlier been added to cart
                if (count > 0)
                {
                    string q2 = @"SELECT Quantity from Cart where ProductID = '" + productId + "' and UserName = '" + userName + "'";
                    SqlCommand cmd2 = new SqlCommand(q2, conn);
                    qty = (int)cmd2.ExecuteScalar();

                    qty++;
                    string q3 = @"UPDATE Cart SET Quantity = '" + qty + "'" + " WHERE ProductID ='" + productId + "' and UserName = '" + userName + "'";
                    SqlCommand cmd3 = new SqlCommand(q3, conn);
                    cmd3.ExecuteNonQuery();
                }
                else
                {
                    // Insert new entry product that has not been added to cart before i.e. quantity = 0 for selected product.
                    string q4 = "INSERT INTO Cart (UserName,ProductID,Quantity)" + "VALUES ('" + userName + "','" + productId + "'," + "'" + qty + "')";
                    SqlCommand cmd4 = new SqlCommand(q4, conn);
                    cmd4.ExecuteNonQuery();
                }
            }
        }

        public static void RemovefromCart(int productId, string userName)
        {
            using (SqlConnection conn = new SqlConnection(Data.db_cfg))
            {
                conn.Open();

                //To get current quantity of items
                string q1 = @"SELECT Quantity from Cart where ProductID = '" + productId + "' and UserName = '" + userName + "'";
                SqlCommand cmd1 = new SqlCommand(q1, conn);
                int qty = (int)cmd1.ExecuteScalar();

                //Even if quantity is  adjusted to 0 under "My purchases page", the items will still be shown so that user can increase it.
                qty--;
                if (qty >= 0)
                {
                    string q2 = @"UPDATE Cart SET Quantity = '" + qty + "'" + " WHERE ProductID ='" + productId + "' and UserName = '" + userName + "'";
                    SqlCommand cmd2 = new SqlCommand(q2, conn);
                    cmd2.ExecuteNonQuery();
                }
            }
        }

        public static void EmptyCart(string userName)
        {
            using (SqlConnection conn = new SqlConnection(Data.db_cfg))
            {
                conn.Open();

                string q1 = @"DELETE from Cart where UserName = '" + userName + "'";
                SqlCommand cmd1 = new SqlCommand(q1, conn);
                cmd1.ExecuteNonQuery();
            }
        }

        public static void UpdateCart(int productId, string userName,int qty)
        {
            using (SqlConnection conn = new SqlConnection(Data.db_cfg))
            {
                conn.Open();
                if (qty >= 0)
                {
                    string q2 = @"UPDATE Cart SET Quantity = '" + qty + "'" + " WHERE ProductID ='" + productId + "' and UserName = '" + userName + "'";
                    SqlCommand cmd2 = new SqlCommand(q2, conn);
                    cmd2.ExecuteNonQuery();
                }
            }
        }

        public static List<Cart> CartList(string userName)
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
                    Product prod = new Product()
                    {
                        ProductId = (int)reader["ProductID"],
                        Description = (string)reader["Description"],
                        Price = (double)reader["Price"],
                        ProductName = (string)reader["ProductName"]
                    };

                    Cart cart = new Cart()
                    {
                        ProductId = (int)reader["ProductID"],
                        UserName = (string)reader["Username"],
                        Quantity = (int)reader["Quantity"],
                        Product = prod

                    };
                    carts.Add(cart);
                }
                reader.Close();
            }
            return carts;
        }
    }
}