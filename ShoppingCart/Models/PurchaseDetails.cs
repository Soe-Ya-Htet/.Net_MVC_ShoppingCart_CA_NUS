using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class PurchaseDetails
    {
        public string AtvCode { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
    }
}