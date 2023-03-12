using Microsoft.EntityFrameworkCore;
using ReportApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp_Repositories.Employees
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
