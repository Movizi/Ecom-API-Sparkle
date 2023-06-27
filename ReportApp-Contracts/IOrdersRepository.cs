using ReportApp_Models;

namespace ReportApp_Contracts
{
    public interface IOrdersRepository
    {
        List<Order> GetOrders();
        string GetOrderById(int id);

    }
}
