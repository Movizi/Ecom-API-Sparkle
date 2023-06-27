using ReportApp_Contracts;
using ReportApp_Models;


namespace ReportApp_Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbContext _context;
        public EmployeeRepository(IDbContext context)
        {
            _context = context;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = _context.Employees.ToList();
            return employees;
        }
    }
}
