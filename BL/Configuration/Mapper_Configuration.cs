using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using BL.DTO;

namespace BL.Configuration
{
    public class Mapper_Configuration : Profile
    {
        public Mapper_Configuration()
        {
            this.CreateMap<Product, Product_DTO>().ReverseMap();
            this.CreateMap<ShoppingCart_Item, ShoppingCartItem_DTO>().ReverseMap();
            this.CreateMap<Shopping_Cart, ShoppingCart_DTO>().ReverseMap();
            this.CreateMap<Order_Item, OrderItem_DTO>().ReverseMap();
            this.CreateMap<Order, Order>().ReverseMap();
        }
    }
}
