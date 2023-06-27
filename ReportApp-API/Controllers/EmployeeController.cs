using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportApp_Models;
using ReportApp_Contracts;

namespace ReportApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public EmployeeController(
            IEmployeeRepository employeeRepository,
            ILoggerManager logger,
            IMapper mapper
            )
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Employee
        //[HttpGet]
        //public IEnumerable<Employee> GetEmployees()
        //{
        //    return _employeeRepository.GetAllEmployees();
        //}
    }
}
