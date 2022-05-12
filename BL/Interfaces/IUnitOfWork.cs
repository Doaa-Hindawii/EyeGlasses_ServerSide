using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Repositories;

namespace BL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Product_Repository Product { get; }
        ShoppingCart_Repository Shopping_Cart { get; }
        ShoppingCart_ItemRepository ShoppingCart_Item { get; }
        Order_Repository Order { get; }
        Order_ItemRepository Order_Item { get; }
        int Commit();

    }
}
