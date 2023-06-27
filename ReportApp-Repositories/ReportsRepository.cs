using ReportApp_Models.Reports;
using System.Data.SqlClient;
using ReportApp_Contracts;

namespace ReportApp_Repositories
{
    public class ReportsRepository : IReportsRepository
    {
        #region Injection
        private readonly IDbContext _context;
        private readonly string _connectionString;
        public ReportsRepository(IDbContext context)
        {
            _context = context;
            _connectionString = _context.ConnectionString();
        }
        #endregion
        public IEnumerable<SalesByCountry> SalesByCountry(DateTime dateFrom, DateTime dateTo)
        {
            var con = new SqlConnection(_connectionString);

            throw new NotImplementedException();
        }
    }
}
