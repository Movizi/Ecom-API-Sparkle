using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReportApp_Models;
using ReportApp_Repositories.Suppliers;

namespace ReportApp_API.Controllers
{
    [Tags("Part III: Suppliers")]
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        #region Injection
        private readonly ISupplierRepository _supplierRepository;
        private readonly ILogger<SupplierController> _logger;
        private readonly IMapper _mapper;
        public SupplierController(
            ISupplierRepository supplierRepository,
            ILogger<SupplierController> logger,
            IMapper mapper
            )
        {
            _supplierRepository = supplierRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        // GET: api/Supplier
        [HttpGet]
        public IEnumerable<Supplier> GetSuppliers()
        {
            return _supplierRepository.GetAll();
        }

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

        // PUT: api/Supplier/5
        [HttpPut("{id}")]
        public IActionResult UpdateSupplier(int id, [FromBody] Supplier supplier)
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

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex}");
                return StatusCode(500, $"Internal server error {ex}");
            }
        }

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
