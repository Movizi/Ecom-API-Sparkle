using ReportApp_Models.Reports;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp_Repositories.Reports
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
