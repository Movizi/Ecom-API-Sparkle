using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReportApp_Models;
using System.Data;
using System.Data.SqlClient;
using ReportApp_Contracts;

namespace ReportApp_Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        #region Injection
        private readonly IDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _connectionsString;
        public OrdersRepository(IDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _connectionsString = _configuration.GetConnectionString("DefaultConnection");
        }
        #endregion

        #region Get
        public List<Order> GetOrders()
        {
            var connection = new SqlConnection(_connectionsString);

            var result = connection.Query<Order>("GetOrders", commandType: CommandType.StoredProcedure).ToList();

            return result;
        }
        public string GetOrderById(int id)
        {
            var connection = new SqlConnection(_connectionsString);

            var param = new DynamicParameters();
            param.Add("@OrderId", id);
            param.Add("@JsonData", dbType: DbType.String, size: 4000, direction: ParameterDirection.Output);

            connection.Execute("GetOrderDetails", param: param, commandType: CommandType.StoredProcedure);

            var result = param.Get<string>("@JsonData");

            return result;
        }
        #endregion

    }
}
