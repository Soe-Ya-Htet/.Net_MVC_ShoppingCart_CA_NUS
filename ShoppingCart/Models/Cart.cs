using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class Cart
    {
        private int productId;
        private string userName;
        private int quantity;
        private Product product;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public Product Product
        {
            get { return product; }
            set { product = value; }

        }
    }
}