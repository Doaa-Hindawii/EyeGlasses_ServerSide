using BL.AppServices;
using BL.DTO;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace EyeGlasses_ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        ShoppingCart_AppService _shoppingcart;
        ShoppingCartItem_AppService _shoppingcartItem;
        IHttpContextAccessor _httpContextAccessor;
        AppDB_Context _context;
        Product_AppService _product;
        public ShoppingCartController(ShoppingCartItem_AppService shoppingcartItem,
                               IHttpContextAccessor httpContextAccessor,
                               ShoppingCart_AppService shoppingcart,
                               AppDB_Context context,
                               Product_AppService product)

        {
            this._shoppingcartItem = shoppingcartItem;
            this._httpContextAccessor = httpContextAccessor;
            this._shoppingcart = shoppingcart;
            this._context = context;
            this._product = product;

        }


        [HttpGet("{userID}")]
        public IActionResult GetAllCartItems(string userID)
        {
            int cartID = _shoppingcart.GetShoppingCartIdByUser(userID);
            return Ok(_shoppingcartItem.GetAllCartItem(cartID));
        }

        [HttpPost("AddItemToCart/{userID}/{productID:int}")]
        public IActionResult CreateItemToCart(int productID, string userID)
        {
            int cartID = _shoppingcart.GetShoppingCartIdByUser(userID);
            Product_DTO product = _product.GetProduct(productID);
            ShoppingCart_Item cartItem = new ShoppingCart_Item();
            cartItem.ShoppingCart_ID = cartID;
            cartItem.Product_ID = productID;
            cartItem.Quantity = 1;

            var isExistingProductCartViewModel = _shoppingcartItem.CheckIfItemExistsInCart(cartID, productID);
            if (isExistingProductCartViewModel == false)
            {
                _shoppingcartItem.SaveNewCartItem(cartItem);
                return Ok();
            }
            return BadRequest("This Item already exist in your cart !");
        }

        [HttpDelete("DeleteProduct/{userID}/{productID:int}")]
        public ActionResult DeleteProductFromCart(string userID, int productID)
        {
            int cartID = _shoppingcart.GetShoppingCartIdByUser(userID);

            var isExistingProductCartViewModel = _shoppingcartItem.CheckIfItemExistsInCart(cartID, productID);
            if (isExistingProductCartViewModel == true)
            {
                _shoppingcartItem.DeleteCartItem(_shoppingcartItem.GetShoppingCartItemID(cartID, productID));
                return Ok();
            }
            return BadRequest("This Item doesn't exist in cart");
        }

        [HttpPut("IncreaseCartItem/{userID}/{productID:int}")]
        public ActionResult IncreaseCartItem(int productID, string userID)
        {
            int cartID = _shoppingcart.GetShoppingCartIdByUser(userID);
            int cartItemId = _shoppingcartItem.GetCartItem(cartID, productID).ID;
            _shoppingcartItem.IncreaseQuantity(cartItemId);
            return Ok();
        }

        [HttpPut("DecreaseCartItem/{userID}/{productID:int}")]
        public ActionResult DecreaseCartItem(int productID, string userID)
        {
            int cartID = _shoppingcart.GetShoppingCartIdByUser(userID);
            int cartItemId = _shoppingcartItem.GetCartItem(cartID, productID).ID;
            _shoppingcartItem.DecreaseQuantity(cartItemId);
            return Ok();
        }

        [HttpGet("GetCartItem/{userID}/{productID:int}")]
        public ActionResult GetCartItem(string userID, int productID)
        {
            int cartID = _shoppingcart.GetShoppingCartIdByUser(userID);
            ShoppingCartItem_DTO cartItem = _shoppingcartItem.GetCartItem(cartID, productID);
            return Ok(cartItem);
        }

        [HttpDelete("ClearCartItems/{userID}")]
        public IActionResult ClearCartItems(string userID)
        {
            int cartID = _shoppingcart.GetShoppingCartIdByUser(userID);
            List<ShoppingCartItem_DTO> cartItems = _shoppingcartItem.GetAllCartItem(cartID);
            foreach (var item in cartItems)
            {
                _shoppingcartItem.DeleteCartItem(item.ID);
            }
            return Ok();
        }
    }
}
