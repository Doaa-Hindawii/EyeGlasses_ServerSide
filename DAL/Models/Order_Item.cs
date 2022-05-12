using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Order_Item
    {
        public int ID { get; set; }
        public double TotalPrice { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Product")]
        public int Product_ID { get; set; }
        public virtual Product Product { get; set; }

        [ForeignKey("Order")]
        public int Order_Id { get; set; }
        public virtual Order Order { get; set; }
    }
}
