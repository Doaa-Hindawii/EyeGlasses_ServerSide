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
    public class Order_ItemRepository : Base_Repository<Order_Item>
    {
        private DbContext Store_DBContext;

        public Order_ItemRepository(DbContext _Store_DBContext) : base(_Store_DBContext)
        {
            this.Store_DBContext = _Store_DBContext;
        }

        public List<Order_Item> GetAllProductsInOrder(int _Order_Id)
        {
            return DbSet.Where(I => I.Order_Id == _Order_Id).Include(I => I.Product).ToList();
        }

        public bool InsertItemInOrder(Order_Item _Order_Item)
        {
            return Insert(_Order_Item);
        }
        public void UpdateItemInOrder(Order_Item _Order_Item)
        {
            Update(_Order_Item);
        }
        public void DeleteItemFromOrder(int id)
        {
            Delete(id);
        }

        public bool CheckItemExistsInOrder(Order_Item _Order_Item)
        {
            return GetAny(i => i.ID == _Order_Item.ID);
        }
        public Order_Item GetItemInOrderById(int id)
        {
            return GetFirstOrDefault(i => i.ID == id);
        }
    }
}