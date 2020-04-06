using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ShoppingCart.Models 
{
    public class UserInfo
    {
        private string userName;
        private string password;
        private string firstName;
        private string lastName;
        private string sessionId;

        [Required(ErrorMessage = "Username is required")]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [Required(ErrorMessage = "Password is required")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string SessionId
        {
            get { return sessionId; }
            set { sessionId = value; }
        }
    }
}