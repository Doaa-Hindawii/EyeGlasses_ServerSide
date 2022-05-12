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
    public class ShoppingCart_ItemRepository : Base_Repository<ShoppingCart_Item>
    {
        private DbContext Store_DBContext;

        public ShoppingCart_ItemRepository(DbContext _Store_DBContext) : base(_Store_DBContext)
        {
            this.Store_DBContext = _Store_DBContext;
        }
        public List<ShoppingCart_Item> GetAllProductsInCart(int _Id)
        {
            return DbSet.Where(c => c.ID == _Id).Include(c => c.Product).ToList();
        }

        public bool InsertItemInCart(ShoppingCart_Item CartItem)
        {
            return Insert(CartItem);
        }

        public void DeleteItemFromCart(int id)
        {
            Delete(id);
        }
        public ShoppingCart_Item GetItemInCartById(int _id)
        {
            return GetFirstOrDefault(c => c.ID == _id);
        }
    }
}
