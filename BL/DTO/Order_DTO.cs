using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class Order_DTO
    {
        public int ID { get; set; }
        public double TotalPrice { get; set; }
        public string User_ID { get; set; }
        public DateTime? date { get; set; }

    }
}
