using Avaliacao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avaliacao.DAL
{
    public interface IOrdersDAL
    {
        IEnumerable<Order> GetAllOrders();
        void AddOrders(IEnumerable<Order> orders);
    }
}
