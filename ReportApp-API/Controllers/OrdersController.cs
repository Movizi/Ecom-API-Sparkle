using Microsoft.AspNetCore.Mvc;
using ReportApp_Contracts;

namespace ReportApp_API.Controllers
{
    [Tags("Part V: Orders")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        #region Injection
        private readonly IOrdersRepository _ordersRepository;
        public OrdersController(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }
        #endregion

        #region Get
        [HttpGet]
        public IActionResult GetOrders()
        {
            var result = _ordersRepository.GetOrders();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderDetails(int id)
        {
            var result = _ordersRepository.GetOrderById(id);
            return Ok(result);
        }
        #endregion
    }
}
