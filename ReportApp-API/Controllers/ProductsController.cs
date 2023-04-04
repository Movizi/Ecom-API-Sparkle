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

        [HttpGet("{categoryName}")]
        public IEnumerable<ProductDto> GetProductsByCategory(string categoryName)
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProductsByCategory(categoryName));
            return products;
        }
    }
}
