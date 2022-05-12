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
    public class Product_Repository : Base_Repository<Product>
    {

        private DbContext Store_DBContext;

        public Product_Repository(DbContext _Store_DBContext) : base(_Store_DBContext)
        {
            this.Store_DBContext = _Store_DBContext;
        }


        public bool CheckProductExists(Product product)
        {
            return GetAny(p => p.ID == product.ID);
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return GetAll().ToList();
        }
        public Product GetProductById(int id)
        {
            var product = DbSet.FirstOrDefault(p => p.ID == id);
            return product;
        }
        public bool InsertProduct(Product product)
        {
            return Insert(product);
        }
        public void UpdateProduct(Product product)
        {
            Update(product);
        }
        public void DeleteProduct(int id)
        {
            Delete(id);
        } 



    }
}
