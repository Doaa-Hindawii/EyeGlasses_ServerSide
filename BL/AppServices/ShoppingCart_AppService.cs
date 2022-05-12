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
    public class ShoppingCart_AppService : Base_ApplicationService
    {
        public ShoppingCart_AppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {
        }

        public List<ShoppingCartItem_DTO> GetAllCarts()
        {

            return Mapper.Map<List<ShoppingCartItem_DTO>>(TheUnitOfWork.Shopping_Cart.GetAllCarts());
        }
        public ShoppingCartItem_DTO GetCart(int id)
        {
            if (id < 0)
                throw new ArgumentNullException();
            return Mapper.Map<ShoppingCartItem_DTO>(TheUnitOfWork.Shopping_Cart.GetById(id));
        }

        public bool CreateUserCart(string _userId)
        {
            bool result = false;
            Shopping_Cart userCart = new Shopping_Cart() { User_ID = _userId };
            if (TheUnitOfWork.Shopping_Cart.Insert(userCart))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }

        public int GetShoppingCartIdByUser(string _userId)
        {
            return (TheUnitOfWork.Shopping_Cart.GetAllCarts().Find(c => c.User_ID == _userId)).ID;
        }


        public bool DeleteCart(int id)
        {
            if (id < 0)
                throw new ArgumentNullException();

            bool result = false;

            TheUnitOfWork.Shopping_Cart.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public IEnumerable<Product_DTO> UserCartProducts(string _userId)
        {
            List<Product_DTO> list = new List<Product_DTO>();
            if (_userId == null)
                return list;
            var cart = TheUnitOfWork.Shopping_Cart.GetFirstOrDefault(c => c.User_ID == _userId);
            if (cart != null)
            {
                var cartItems = TheUnitOfWork.ShoppingCart_Item.GetWhere(c => c.ShoppingCart_ID == cart.ID);
                foreach (var c in cartItems)
                {
                    list.Add(Mapper.Map<Product_DTO>(TheUnitOfWork.Product.GetById(c.Product_ID)));
                }
            }
            return list;
        }
        
    }
}
