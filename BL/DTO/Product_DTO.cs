using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class Product_DTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }

    }
}