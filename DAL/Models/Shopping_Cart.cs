using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Shopping_Cart")]
    public class Shopping_Cart
    {
       public int ID { get; set; }
       public List<ShoppingCart_Item> ShoppingCart_Items { get; set; }

       [ForeignKey("User")]
       public string User_ID { get; set; }
       public User_Identity User { get; set; }
    }
}
