using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ShoppingCart_Item
    {
        [Key]
        public int ID { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public double Price { get; set; }
        [ForeignKey("Product")]
        public int Product_ID { get; set; }

        [ForeignKey("Shopping_Cart")]
        public int ShoppingCart_ID { get; set; }
        public virtual Product Product { get; set; }
        public virtual Shopping_Cart Shopping_Cart { get; set; }
    }
}
