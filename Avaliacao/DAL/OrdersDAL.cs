using Avaliacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacao.DAL
{
    public class OrdersDAL : IOrdersDAL
    {
        public OrdersDAL() { }

        private readonly AppDbContext db;
        public OrdersDAL(AppDbContext context)
        {
            db = context;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            try
            {
                return db.Orders.ToList();
            }
            catch { throw; }
        }

        public void AddOrders(IEnumerable<Order> orders)
        {
            try
            {
                db.Orders.AddRange(orders);
                db.SaveChanges();
            }
            catch { throw; }
        }
    }
}
