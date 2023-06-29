using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportApp_Contracts;
using ReportApp_Models;
using ReportApp_Models.Dtos;

namespace ReportApp_API.Controllers
{
    [Tags("Part I: Categories")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CategoryController(
            ICategoryRepository categoryRepository,
            ILoggerManager logger,
            IMapper mapper
            )
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>List of categories</returns>
        // GET: api/Category
        [HttpGet]
        public IEnumerable<Category> GetCategories()
        {
            return _categoryRepository.GetAllCategories();
        }

        /// <summary>
        /// Get a specific category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category</returns>
        // GET: api/Category/5
        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            var methodName = nameof(GetCategory);
            var category = _categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                _logger.LogWarn($"{methodName} => Category not found");
                return NotFound();
            }

            return category;
        }

        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns>Category</returns>
        // POST: api/Category
        [HttpPost]
        public ActionResult<Category> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            var methodName = nameof(CreateCategory);
            if (categoryDto == null)
            {
                _logger.LogWarn($"{methodName} => category object is null");
                return BadRequest("category object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn($"{methodName} => Invalid model Object");
                return BadRequest("Invalid model Object");
            }

            var category = _mapper.Map<Category>(categoryDto);

            _categoryRepository.AddCategory(category);

            return CreatedAtAction(nameof(CreateCategory), new { id = category.CategoryID }, category);

        }

        /// <summary>
        /// Update existing category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns>Category</returns>
        // PUT: api/Category/5
        [HttpPut("{id}")]
        public ActionResult<Category> UpdateCategory(int id, [FromBody] Category category)
        {
            var methodName = nameof(UpdateCategory);
            if (id != category.CategoryID)
            {
                _logger.LogWarn($"{methodName} => id and CategoryID must be the same");
                return BadRequest("id and CategoryID must be the same");
            }

            if (category == null)
            {
                _logger.LogWarn($"{methodName} => category object is null");
                return BadRequest("category object is null");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarn($"{methodName} => Invalid model Object");
                return BadRequest("Invalid model Object");
            }

            _categoryRepository.UpdateCategory(category);

            return Ok(category);

        }

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code</returns>
        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var methodName = nameof(DeleteCategory);

            if (id == 0)
            {
                _logger.LogWarn($"{methodName} => Id must be valid");
                return BadRequest("Id must be valid");
            }
            var category = _categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                _logger.LogWarn($"{methodName} => Can't find category");
                return NotFound("Can't find category");
            }

            _categoryRepository.DeleteCategory(id);

            return NoContent();
        }
    }
}
