using Microsoft.AspNetCore.Mvc;
using ReportApp_Models;
using ReportApp_Repositories.Categories;

namespace ReportApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
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
        public ActionResult<Category> CreateCategory(Category category)
        {
            _categoryRepository.AddCategory(category);

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryID }, category);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, Category category)
        {
            if (id != category.CategoryID)
            {
                return BadRequest();
            }

            _categoryRepository.UpdateCategory(category);

            return NoContent();
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            _categoryRepository.DeleteCategory(id);

            return NoContent();
        }
    }
}
