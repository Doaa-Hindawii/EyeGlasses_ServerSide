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
    public class OrderItem_AppService : Base_ApplicationService
    {
        public OrderItem_AppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {
        }
        public List<OrderItem_DTO> GetAllOrderItems(int _Id)
        {
            return Mapper.Map<List<OrderItem_DTO>>(TheUnitOfWork.Order_Item.GetAllProductsInOrder(_Id));
        }
        public OrderItem_DTO GetOrderItem(int Order_Id, int Product_Id)
        {
            return Mapper.Map<OrderItem_DTO>(TheUnitOfWork.Order_Item.GetFirstOrDefault(c => c.Order_Id == Order_Id && c.Product_ID == Product_Id));
        }

        public bool SaveNewOrderItem(Order_Item order_Item)
        {
            if (order_Item == null)
                throw new ArgumentNullException();
            bool result = false;
            if (TheUnitOfWork.Order_Item.Insert(order_Item))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool DeleteOrderItem(int id)
        {
            if (id <= 0)
                throw new InvalidOperationException();
            bool result = false;

            TheUnitOfWork.Order_Item.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public int GetOrderItemID(int Order_ID, int Product_ID)
        {
            return TheUnitOfWork.Order_Item
                .GetFirstOrDefault(c => c.Order_Id == Order_ID && c.Product_ID == Product_ID).ID;
        }

    }
}
