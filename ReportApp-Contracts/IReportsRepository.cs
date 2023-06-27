using ReportApp_Models.Reports;

namespace ReportApp_Contracts
{
    public interface IReportsRepository
    {
        IEnumerable<SalesByCountry> SalesByCountry(DateTime dateFrom, DateTime dateTo);
    }
}
