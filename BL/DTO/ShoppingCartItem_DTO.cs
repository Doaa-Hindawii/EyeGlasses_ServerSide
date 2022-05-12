using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class ShoppingCartItem_DTO
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public int Product_ID { get; set; }
        public int ShoppingCart_ID { get; set; }

    }
}