using BL.Interfaces;
using DAL.Models;
using BL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bases
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext DbContext { get; set; }
        private UserManager<User_Identity> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UnitOfWork(AppDB_Context DbContext, UserManager<User_Identity> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this.DbContext = DbContext;
        }

        public ShoppingCart_Repository shopping_cart;
        public ShoppingCart_Repository Shopping_Cart
        {
            get
            {
                if (shopping_cart == null)
                    shopping_cart = new ShoppingCart_Repository(DbContext);
                return shopping_cart;
            }
        }

        public ShoppingCart_ItemRepository cart_item;
        public ShoppingCart_ItemRepository ShoppingCart_Item
        {
            get
            {
                if (cart_item == null)
                    cart_item = new ShoppingCart_ItemRepository(DbContext);
                return cart_item;
            }
        }

        public Product_Repository product;
        public Product_Repository Product
        {
            get
            {
                if (product == null)
                    product = new Product_Repository(DbContext);
                return product;
            }
        }

        public Order_Repository order;
        public Order_Repository Order
        {
            get
            {
                if (order == null)
                    order = new Order_Repository(DbContext);
                return order;
            }
        }
        public Order_ItemRepository order_item;
        public Order_ItemRepository Order_Item
        {
            get
            {
                if (order_item == null)
                    order_item = new Order_ItemRepository(DbContext);
                return order_item;
            }
        }
        public int Commit()
        {
            return DbContext.SaveChanges();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

    }
}