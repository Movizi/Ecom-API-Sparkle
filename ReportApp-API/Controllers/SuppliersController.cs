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
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public SuppliersController(
            ISupplierRepository supplierRepository,
            ILoggerManager logger,
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
        public IActionResult GetSuppliers()
        {
            var suppliers = _supplierRepository.GetAll();
            return Ok(suppliers);
        }

        /// <summary>
        /// Get a specific supplier by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Supplier</returns>
        // GET: api/Supplier/5
        [HttpGet("{id}")]
        public IActionResult GetSupplierById(int id)
        {
            var supplier = _supplierRepository.GetSupplierById(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        /// <summary>
        /// Create a new supplier
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns>Product</returns>
        // POST: api/Supplier
        [HttpPost]
        public IActionResult CreateSupplier([FromBody] Supplier supplier)
        {
            var methodName = nameof(CreateSupplier);

            if (supplier == null)
            {
                _logger.LogWarn($"{methodName} => Supplier object is null");
                return BadRequest("Supplier object is null");
            }

            _supplierRepository.AddSupplier(supplier);

            return CreatedAtAction(nameof(CreateSupplier), new { id = supplier.SupplierID }, supplier);

        }

        /// <summary>
        /// Update existing supplier
        /// </summary>
        /// <param name="id"></param>
        /// <param name="supplier"></param>
        /// <returns>Supplier</returns>
        // PUT: api/Supplier/5
        [HttpPut("{id}")]
        public IActionResult UpdateSupplier(int id, [FromBody] Supplier supplier)
        {
            var methodName = nameof(UpdateSupplier);

            if (id != supplier.SupplierID)
            {
                _logger.LogWarn($"{methodName} => Id and SupplierID must be the same");
                return BadRequest("Id and SupplierID must be the same");
            }

            if (supplier == null)
            {
                _logger.LogWarn($"{methodName} => Supplier object is null");
                return BadRequest("Supplier object is null");
            }

            _supplierRepository.UpdateSupplier(supplier);

            return Ok(supplier);
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
            var methodName = nameof(DeleteSupplier);

            if (id == 0)
            {
                _logger.LogWarn($"{methodName} => Id must be valid");
                return BadRequest("Id must be valid");
            }
            var supplier = _supplierRepository.GetSupplierById(id);

            if (supplier == null)
            {
                _logger.LogWarn($"{methodName} => Can't find suppleir");
                return NotFound("Can't find suppleir");
            }

            _supplierRepository.DeleteSupplier(id);

            return NoContent();
        }
    }
}
