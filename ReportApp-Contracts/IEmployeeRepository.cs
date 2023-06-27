using ReportApp_Models;

namespace ReportApp_Contracts
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
    }
}
