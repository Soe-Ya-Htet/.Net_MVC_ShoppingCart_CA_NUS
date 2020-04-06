using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace ShoppingCart.Models
{
    //Handles user-related queries
    public class UserData
    {
        public static UserInfo GetUserPassword(string UserName)
        {
            UserInfo user = null;

            using (SqlConnection conn = new SqlConnection(Data.db_cfg))
            {
                conn.Open();

                string q1 = @"SELECT Username,Password from UserInfo where Username = '" + UserName + "'";
                SqlCommand cmd1 = new SqlCommand(q1, conn);

                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.Read())
                {
                    user = new UserInfo()
                    {
                        Password = (string)reader["Password"]
                    };
                }
            }
            return user;
        }

        public static UserInfo GetUserFirstandLastNames(string userName)
        {
            UserInfo user = new UserInfo();

            using (SqlConnection conn = new SqlConnection(Data.db_cfg))
            {
                conn.Open();
                string q1 = @"SELECT FirstName from UserInfo where Username = '" + userName + "'";
                SqlCommand cmd1 = new SqlCommand(q1, conn);
                user.FirstName = (string)cmd1.ExecuteScalar();

                string q2 = @"SELECT LastName from UserInfo where Username = '" + userName + "'";
                SqlCommand cmd2 = new SqlCommand(q2, conn);
                user.LastName = (string)cmd2.ExecuteScalar();
            }
            return user;
        }
    }
}