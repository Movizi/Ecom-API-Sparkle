using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportApp_Models;
using ReportApp_Models.Dtos;
using ReportApp_Contracts;

namespace ReportApp_API.Controllers
{
    [Tags("Part II: Products")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : Controller
    {
        #region Injection
        private readonly IProductRepository _productRepository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public ProductsController(
            IProductRepository productRepository,
            ILoggerManager logger,
            IMapper mapper
            )
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of products</returns>
        // GET: api/Products
        [HttpGet]
        public IEnumerable<ProductDto> GetProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetAllProducts());
            return products;
        }
        
        /// <summary>
        /// Get a specific product by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Product</returns>
        // GET api/Products/5
        [HttpGet("{id}")]
        public Product GetProductsById(int id)
        {
            var products = _productRepository.GetProductById(id);
            return products;
        }

        /// <summary>
        /// Get products by category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>products with the specified category</returns>
        // GET: api/Products/categoryId
        [HttpGet("{categoryId}")]
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            var products = _productRepository.GetProductsByCategory(categoryId);
            return products;
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Product</returns>
        // POST: api/Products
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("category object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model Object");
                }


                _productRepository.AddProduct(product);

                return CreatedAtAction(nameof(CreateProduct), new { id = product.ProductID }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex}");
                return StatusCode(500, $"Internal server error {ex}");
            }
        }

        /// <summary>
        /// Update existing product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>Product</returns>
        // PUT: api/Products/5
        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                if (id != product.ProductID)
                {
                    return BadRequest("id and CategoryID must be the same");
                }

                if (product == null)
                {
                    return BadRequest("category object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model Object");
                }

                _productRepository.UpdateProduct(product);

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex}");
                return StatusCode(500, $"Internal server error {ex}");
            }
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code</returns>
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Id must be valid");
                }
                var category = _productRepository.GetProductById(id);

                if (category == null)
                {
                    return NotFound("Can't find category");
                }

                _productRepository.DeleteProduct(id);

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
