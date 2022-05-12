using BL.Bases;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class ShoppingCart_Repository : Base_Repository<Shopping_Cart>
    {
        private DbContext Store_DBContext;

        public ShoppingCart_Repository(DbContext _Store_DBContext) : base(_Store_DBContext)
        {
            this.Store_DBContext = _Store_DBContext;
        }

        public List<Shopping_Cart> GetAllCarts()
        {
            return GetAll().ToList();
        }

        public bool InsertCart(Shopping_Cart cart)
        {
            return Insert(cart);
        }
        public void UpdateCart(Shopping_Cart cart)
        {
            Update(cart);
        }
        public void DeleteCart(int id)
        {
            Delete(id);
        }

        public bool CheckCartExists(Shopping_Cart _shoppingcart)
        {
            return GetAny(c => c.ID == _shoppingcart.ID);
        }
        public Shopping_Cart GetCartById(int _id)
        {
            return GetFirstOrDefault(c => c.ID == _id);
        }
    }
}
