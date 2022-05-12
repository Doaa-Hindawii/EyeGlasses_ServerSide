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
    public class Product_AppService : Base_ApplicationService
    {
        public Product_AppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {
        }
        public IEnumerable<Product_DTO> GetAllProduct()
        {
            IEnumerable<Product> allProducts = TheUnitOfWork.Product.GetAllProduct();
            return Mapper.Map<IEnumerable<Product_DTO>>(allProducts);
        }

        public Product_DTO GetProduct(int id)
        {
            return Mapper.Map<Product_DTO>(TheUnitOfWork.Product.GetProductById(id));
        }

        public bool SaveNewProduct(Product_DTO product_DTO)
        {
            if (product_DTO == null)
                throw new ArgumentNullException();
            bool result = false;
            var product = Mapper.Map<Product>(product_DTO);
            if (TheUnitOfWork.Product.Insert(product))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }

        public bool UpdateProduct(Product_DTO product_DTO)
        {
            var product = TheUnitOfWork.Product.GetById(product_DTO.ID);
            Mapper.Map(product_DTO, product);
            TheUnitOfWork.Product.Update(product);
            TheUnitOfWork.Commit();
            return true;
        }
        public bool DecreaseQuantity(int _ID, int decresedQuantity)
        {
            var product = TheUnitOfWork.Product.GetById(_ID);
            product.Quantity -= decresedQuantity;
            TheUnitOfWork.Product.Update(product);
            TheUnitOfWork.Commit();
            return true;
        }
        public bool DeleteProduct(int _id)
        {
            bool result = false;

            TheUnitOfWork.Product.Delete(_id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public bool CheckProductExists(Product_DTO product_DTO)
        {
            Product product = Mapper.Map<Product>(product_DTO);
            return TheUnitOfWork.Product.CheckProductExists(product);
        }

    }
}
