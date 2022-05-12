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
    public class Order_AppService : Base_ApplicationService
    {
        public Order_AppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {
        }

        public List<Order_DTO> GetAllOrders()
        {

            return Mapper.Map<List<Order_DTO>>(TheUnitOfWork.Order.GetAllOrders());
        }
        public Order_DTO GetOrder(int id)
        {
            if (id < 0)
                throw new ArgumentNullException();
            return Mapper.Map<Order_DTO>(TheUnitOfWork.Order.GetById(id));
        }

        public bool CreateUserOrder(string _userId, double TotalPrice)
        {
            bool result = false;
            Order order = new Order() { User_ID = _userId, TotalPrice = TotalPrice };
            if (TheUnitOfWork.Order.Insert(order))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }

        public int GetOrderIdByUser(string _userId)
        {
            List<Order> orders = new List<Order>();
            orders = Mapper.Map<List<Order>>(TheUnitOfWork.Order.GetAllOrders().Where(c => c.User_ID == _userId));
            var result = orders.OrderByDescending(i => i.ID).FirstOrDefault();
            return result.ID;
        }


        public bool Delete(int id)
        {
            if (id < 0)
                throw new ArgumentNullException();

            bool result = false;

            TheUnitOfWork.Order.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

    }
}
