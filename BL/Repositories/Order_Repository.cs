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
    public class Order_Repository : Base_Repository<Order>
    {
        private DbContext Store_DBContext;

        public Order_Repository(DbContext _Store_DBContext) : base(_Store_DBContext)
        {
            this.Store_DBContext = _Store_DBContext;
        }

        public List<Order> GetAllOrders()
        {
            return GetAll().ToList();
        }

        public bool InsertOrder(Order _order)
        {
            return Insert(_order);
        }
        public void UpdateOrder(Order _order)
        {
            Update(_order);
        }
        public void DeleteOrder(int _id)
        {
            Delete(_id);
        }

        public bool CheckOrderExists(Order _order)
        {
            return GetAny(i => i.ID == _order.ID);
        }
        public Order GetOrderById(int _id)
        {
            return GetFirstOrDefault(i => i.ID == _id);
        }
    }
}
