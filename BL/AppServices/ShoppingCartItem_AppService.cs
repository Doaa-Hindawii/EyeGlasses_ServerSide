using AutoMapper;
using BL.Bases;
using BL.DTO;
using BL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class ShoppingCartItem_AppService : Base_ApplicationService
    {
        public ShoppingCartItem_AppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {
        }

        public List<ShoppingCartItem_DTO> GetAllCartItem(int _shoppingcartId)
        {

            return Mapper.Map<List<ShoppingCartItem_DTO>>(TheUnitOfWork.ShoppingCart_Item.GetAllProductsInCart(_shoppingcartId));
        }
        public ShoppingCartItem_DTO GetCartItem(int _shoppingcartId, int _productId)
        {
            return Mapper.Map<ShoppingCartItem_DTO>(TheUnitOfWork.ShoppingCart_Item.GetFirstOrDefault(c => c.ShoppingCart_ID == _shoppingcartId && c.Product_ID == _productId));
        }
        public bool SaveNewCartItem(ShoppingCart_Item _shoppingcartItem)
        {
            if (_shoppingcartItem == null)
                throw new ArgumentNullException();
            bool result = false;
            if (TheUnitOfWork.ShoppingCart_Item.Insert(_shoppingcartItem))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool DeleteCartItem(int id)
        {
            if (id <= 0)
                throw new InvalidOperationException();
            bool result = false;

            TheUnitOfWork.ShoppingCart_Item.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckIfItemExistsInCart(int _shoppingcartID, int _productID)
        {
            var isExistItemInCart = TheUnitOfWork.ShoppingCart_Item
                .GetFirstOrDefault(c => c.ShoppingCart_ID == _shoppingcartID && c.Product_ID == _productID);
            return isExistItemInCart != null;
        }

        public int GetShoppingCartItemID(int _cartID, int _productID)
        {
            return TheUnitOfWork.ShoppingCart_Item
                .GetFirstOrDefault(c => c.ShoppingCart_ID == _cartID && c.Product_ID == _productID).ID;
        }
        public bool DecreaseQuantity(int _shoppingcartItemId)
        {
            var cartItem = TheUnitOfWork.ShoppingCart_Item.GetById(_shoppingcartItemId);
            cartItem.Quantity--;
            cartItem.TotalPrice -= cartItem.Price;
            TheUnitOfWork.ShoppingCart_Item.Update(cartItem);
            TheUnitOfWork.Commit();
            return true;
        }
        public bool IncreaseQuantity(int _shoppingcartItemId)
        {
            var cartItem = TheUnitOfWork.ShoppingCart_Item.GetFirstOrDefault(c => c.ID == _shoppingcartItemId);
            cartItem.Quantity++;
            cartItem.TotalPrice += cartItem.Price;
            TheUnitOfWork.ShoppingCart_Item.Update(cartItem);
            TheUnitOfWork.Commit();
            return true;
        }

    }
}
