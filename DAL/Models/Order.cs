using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        public double TotalPrice { get; set; }

        [ForeignKey("User")]
        public string User_ID { get; set; }
        public virtual User_Identity User { get; set; }
        public virtual ICollection<Order_Item> order_items { get; set; }
    }
}
