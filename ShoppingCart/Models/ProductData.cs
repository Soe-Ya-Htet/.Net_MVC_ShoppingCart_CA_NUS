using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace ShoppingCart.Models
{
    //Handles queries pertaining to Product
    public class ProductData
    {
        private static List<Product> _GetProductDetails(string input)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(Data.db_cfg))
            {
                conn.Open();

                string q = @"SELECT * from Product where ProductName LIKE '%" + input + "%' OR Description LIKE '%" + input + "%'";
                SqlCommand cmd = new SqlCommand(q, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductId = (int)reader["ProductID"],
                        ProductName = (string)reader["ProductName"],
                        Description = (string)reader["Description"],
                        Price = Convert.ToDouble(reader["Price"])
                    };
                    products.Add(product);
                }
            }
            return products;
        }

        public static List<Product> GetProductDetails(string input)
        {
            return ProductData._GetProductDetails(input);
        }

        public static List<Product> GetProductDetails()
        {
            return ProductData._GetProductDetails("");
        }

    }
}