using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReportApp_Models.Dtos;
using ReportApp_Models;
using ReportApp_Contracts;
using Microsoft.AspNetCore.Authorization;

namespace ReportApp_API.Controllers
{
    [Tags("Part IV: Shippers")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShippersController : ControllerBase
    {
        private readonly IShipperRepository _shipperRepository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public ShippersController(
            IShipperRepository shipperRepository,
            ILoggerManager logger,
            IMapper mapper
            )
        {
            _shipperRepository = shipperRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all shippers
        /// </summary>
        /// <returns>List of shippers</returns>
        // GET: api/Shipper
        [HttpGet]
        public IActionResult GetShippers()
        {
            var shippers = _shipperRepository.GetAll();
            return Ok(shippers);
        }

        /// <summary>
        /// Get a specific shipper by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Shipper</returns>
        // GET: api/Shipper/5
        [HttpGet("{id}")]
        public IActionResult GetShipper(int id)
        {
            var Shipper = _shipperRepository.GetShipperById(id);

            if (Shipper == null)
            {
                return NotFound();
            }

            return Ok(Shipper);
        }

        /// <summary>
        /// Create a new shipper
        /// </summary>
        /// <param name="shipperDto"></param>
        /// <returns>Shipper</returns>
        // POST: api/Shipper
        [HttpPost]
        public IActionResult CreateShipper([FromBody] ShipperDto shipperDto)
        {
            var methodName = nameof(CreateShipper);

            if (shipperDto == null)
            {
                _logger.LogWarn($"{methodName} => Shipper object is null");
                return BadRequest("Shipper object is null");
            }

            var shipper = _mapper.Map<Shipper>(shipperDto);

            _shipperRepository.AddShipper(shipper);

            return CreatedAtAction(nameof(CreateShipper), new { id = shipper.ShipperID }, shipper);
        }

        /// <summary>
        /// Update existing shipper
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shipper"></param>
        /// <returns>Shipper</returns>
        // PUT: api/Shipper/5
        [HttpPut("{id}")]
        public IActionResult UpdateShipper(int id, [FromBody] Shipper shipper)
        {
            var methodName = nameof(UpdateShipper);

            if (id != shipper.ShipperID)
            {
                _logger.LogWarn($"{methodName} => Id and ShipperID must be the same");
                return BadRequest("Id and ShipperID must be the same");
            }

            if (shipper == null)
            {
                _logger.LogWarn($"{methodName} => Shipper object is null");
                return BadRequest("Shipper object is null");
            }

            _shipperRepository.UpdateShipper(shipper);

            return Ok(shipper);
        }

        /// <summary>
        /// Delete a shipper
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code</returns>
        // DELETE: api/Shipper/5
        [HttpDelete("{id}")]
        public IActionResult DeleteShipper(int id)
        {
            var methodName = nameof(DeleteShipper);
            if (id == 0)
            {
                _logger.LogWarn($"{methodName} => Id must be valid");
                return BadRequest("Id must be valid");
            }
            var Shipper = _shipperRepository.GetShipperById(id);

            if (Shipper == null)
            {
                _logger.LogWarn($"{methodName} => Can't find Shipper");
                return NotFound("Can't find Shipper");
            }

            _shipperRepository.DeleteShipper(id);

            return NoContent();
        }
    }
}
