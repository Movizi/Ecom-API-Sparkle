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
        private readonly ILogger<ShippersController> _logger;
        private readonly IMapper _mapper;
        public ShippersController(
            IShipperRepository shipperRepository,
            ILogger<ShippersController> logger,
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
        public IEnumerable<Shipper> GetShippers()
        {
            return _shipperRepository.GetAll();
        }

        /// <summary>
        /// Get a specific shipper by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Shipper</returns>
        // GET: api/Shipper/5
        [HttpGet("{id}")]
        public ActionResult<Shipper> GetShipper(int id)
        {
            var Shipper = _shipperRepository.GetShipperById(id);

            if (Shipper == null)
            {
                return NotFound();
            }

            return Shipper;
        }

        /// <summary>
        /// Create a new shipper
        /// </summary>
        /// <param name="shipperDto"></param>
        /// <returns>Shipper</returns>
        // POST: api/Shipper
        [HttpPost]
        public ActionResult<Shipper> CreateShipper([FromBody] ShipperDto shipperDto)
        {
            try
            {
                if (shipperDto == null)
                {
                    return BadRequest("Shipper object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model Object");
                }

                var shipper = _mapper.Map<Shipper>(shipperDto);

                _shipperRepository.AddShipper(shipper);

                return CreatedAtAction(nameof(CreateShipper), new { id = shipper.ShipperID }, shipper);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex}");
                return StatusCode(500, $"Internal server error {ex}");
            }
        }

        /// <summary>
        /// Update existing shipper
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shipper"></param>
        /// <returns>Shipper</returns>
        // PUT: api/Shipper/5
        [HttpPut("{id}")]
        public ActionResult<Shipper> UpdateShipper(int id, [FromBody] Shipper shipper)
        {
            try
            {
                if (id != shipper.ShipperID)
                {
                    return BadRequest("id and ShipperID must be the same");
                }

                if (shipper == null)
                {
                    return BadRequest("Shipper object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model Object");
                }

                _shipperRepository.UpdateShipper(shipper);

                return Ok(shipper);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex}");
                return StatusCode(500, $"Internal server error {ex}");
            }
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
            try
            {
                if (id == 0)
                {
                    return BadRequest("Id must be valid");
                }
                var Shipper = _shipperRepository.GetShipperById(id);

                if (Shipper == null)
                {
                    return NotFound("Can't find Shipper");
                }

                _shipperRepository.DeleteShipper(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex}");
                return StatusCode(500, $"Internal server error {ex}");
            }
        }
    }
}
