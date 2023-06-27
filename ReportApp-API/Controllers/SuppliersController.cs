using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportApp_Models;
using ReportApp_Contracts;

namespace ReportApp_API.Controllers
{
    [Tags("Part III: Suppliers")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SuppliersController : ControllerBase
    {
        #region Injection
        private readonly ISupplierRepository _supplierRepository;
        private readonly ILogger<SuppliersController> _logger;
        private readonly IMapper _mapper;
        public SuppliersController(
            ISupplierRepository supplierRepository,
            ILogger<SuppliersController> logger,
            IMapper mapper
            )
        {
            _supplierRepository = supplierRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        /// <summary>
        /// Get all suppliers
        /// </summary>
        /// <returns>List of suppliers</returns>
        // GET: api/Supplier
        [HttpGet]
        public IEnumerable<Supplier> GetSuppliers()
        {
            return _supplierRepository.GetAll();
        }

        /// <summary>
        /// Get a specific supplier by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Supplier</returns>
        // GET: api/Supplier/5
        [HttpGet("{id}")]
        public ActionResult<Supplier> GetSupplierById(int id)
        {
            var supplier = _supplierRepository.GetSupplierById(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return supplier;
        }

        /// <summary>
        /// Create a new supplier
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns>Product</returns>
        // POST: api/Supplier
        [HttpPost]
        public ActionResult<Supplier> CreateSupplier([FromBody] Supplier supplier)
        {
            try
            {
                if (supplier == null)
                {
                    return BadRequest("supplier object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model Object");
                }


                _supplierRepository.AddSupplier(supplier);

                return CreatedAtAction(nameof(CreateSupplier), new { id = supplier.SupplierID}, supplier);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex}");
                return StatusCode(500, $"Internal server error {ex}");
            }

        }

        /// <summary>
        /// Update existing supplier
        /// </summary>
        /// <param name="id"></param>
        /// <param name="supplier"></param>
        /// <returns>Supplier</returns>
        // PUT: api/Supplier/5
        [HttpPut("{id}")]
        public ActionResult<Supplier> UpdateSupplier(int id, [FromBody] Supplier supplier)
        {
            try
            {
                if (id != supplier.SupplierID)
                {
                    return BadRequest("id and SupplierID must be the same");
                }

                if (supplier == null)
                {
                    return BadRequest("supplier object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model Object");
                }

                _supplierRepository.UpdateSupplier(supplier);

                return Ok(supplier);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex}");
                return StatusCode(500, $"Internal server error {ex}");
            }
        }

        /// <summary>
        /// Delete a supplier
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code</returns>
        // DELETE: api/Supplier/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSupplier(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Id must be valid");
                }
                var supplier = _supplierRepository.GetSupplierById(id);

                if (supplier == null)
                {
                    return NotFound("Can't find suppleir");
                }

                _supplierRepository.DeleteSupplier(id);

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
