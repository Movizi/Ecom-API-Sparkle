using ReportApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp_Repositories.Orders
{
    public interface IOrdersRepository
    {
        List<Order> GetOrders();
        string GetOrderById(int id);

    }
}
