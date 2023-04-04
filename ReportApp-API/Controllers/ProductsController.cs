using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReportApp_Models;
using ReportApp_Models.Dtos;
using ReportApp_Repositories.Categories;
using ReportApp_Repositories.Products;

namespace ReportApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;
        public ProductsController(
            IProductRepository productRepository,
            ILogger<CategoryController> logger,
            IMapper mapper
            )
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<ProductDto> GetProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetAllProducts());
            return products;
        }

        [HttpGet("{categoryId}")]
        public IEnumerable<ProductDto> GetProductsByCategory(int categoryId)
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProductsByCategory(categoryId));
            return products;
        }

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
    }
}
