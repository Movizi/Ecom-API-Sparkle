using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportApp_Models;
using ReportApp_Models.Dtos;
using ReportApp_Models.Profiles;
using ReportApp_Repositories.Categories;

namespace ReportApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryController> _logger;
        private readonly IMapper _mapper;
        public CategoryController(
            ICategoryRepository categoryRepository,
            ILogger<CategoryController> logger,
            IMapper mapper
            )
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Category
        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // POST: api/Category
        [HttpPost]
        public ActionResult<Category> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                if (categoryDto == null)
                {
                    return BadRequest("category object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model Object");
                }

                var category = _mapper.Map<Category>(categoryDto);

                _categoryRepository.AddCategory(category);

                return CreatedAtAction(nameof(CreateCategory), new { id = category.CategoryID }, category);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex}");
                return StatusCode(500, $"Internal server error {ex}");
            }

        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category category)
        {
            try
            {
                if (id != category.CategoryID)
                {
                    return BadRequest("id and CategoryID must be the same");
                }

                if (category == null)
                {
                    return BadRequest("category object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model Object");
                }

                _categoryRepository.UpdateCategory(category);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex}");
                return StatusCode(500, $"Internal server error {ex}");
            }
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Id must be valid");
                }
                var category = _categoryRepository.GetCategoryById(id);

                if (category == null)
                {
                    return NotFound("Can't find category");
                }

                _categoryRepository.DeleteCategory(id);

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
