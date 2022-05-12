using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL.AppServices;
using BL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EyeGlasses_ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {

        Order_AppService _order;
        ShoppingCartItem_AppService _shoppingcartitem;
        OrderItem_AppService _orderItem;
        Product_AppService _product;
        public OrderController(OrderItem_AppService orderItem,
                               Order_AppService order,
                               AppDB_Context context,
                               Product_AppService product,
                               ShoppingCartItem_AppService shoppingcartItem)

        {
            this._orderItem = orderItem;
            this._order = order;
            this._product = product;
            this._shoppingcartitem = shoppingcartItem;
        }

        [HttpPost("CreateOrder/{userID}/{TotalPrice}")]
        public IActionResult CreateOrder(string userID, double TotalPrice)
        {
            _order.CreateUserOrder(userID, TotalPrice);
            return Ok();
        }
        [HttpPost("AddItemInOrder/{Product_ID:int}/{ShoppingCart_Id:int}")]
        public IActionResult AddItemInOrder(int Product_ID, int ShoppingCart_Id)
        {
            Product_DTO product = _product.GetProduct(Product_ID);
            Order_Item orderItem = new Order_Item();
            var catItem = _shoppingcartitem.GetCartItem(ShoppingCart_Id, Product_ID);
            orderItem.Product_ID = Product_ID;
            orderItem.Quantity = catItem.Quantity;
            _product.UpdateProduct(product);
            orderItem.Price = product.Price;
            orderItem.TotalPrice = catItem.Quantity * product.Price;
            _orderItem.SaveNewOrderItem(orderItem);
            _shoppingcartitem.DeleteCartItem(catItem.ID);
            return Ok(orderItem);
        }

        [HttpGet("GetOrderItems/{Order_Id:int}")]
        public ActionResult GetOrderItems(int Order_Id, string sellername)
        {
            var orderItems = _orderItem.GetAllOrderItems(Order_Id);
            List<Product_DTO> products = new List<Product_DTO>();
            foreach (var item in orderItems)
            {

                products.Add(_product.GetProduct(item.Product_ID));
            }

            return Ok(products);
        }

        [HttpGet("GetAllOrderItems/{Order_Id:int}")]
        public ActionResult GetAllOrderItems(int Order_Id)
        {
            return Ok(_orderItem.GetAllOrderItems(Order_Id));
        }

        [HttpGet("GetAllOrders/{Order_Id:int}")]
        public ActionResult GetAllOrders(int Order_Id)
        {
            return Ok(_order.GetAllOrders());
        }
    }
}
