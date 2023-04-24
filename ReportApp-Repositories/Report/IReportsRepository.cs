using ReportApp_Models.Reports;

namespace ReportApp_Repositories.Reports
{
    public interface IReportsRepository
    {
        IEnumerable<SalesByCountry> SalesByCountry(DateTime dateFrom, DateTime dateTo);
    }
}
