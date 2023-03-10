using Microsoft.EntityFrameworkCore;
using ReportApp_Models;
using System.IO.Compression;
using System.Text;

namespace ReportApp_Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbContext _context;

        public CategoryRepository(IDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.Find(categoryId);
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _context.SetEntityState(category,EntityState.Modified);
            _context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}
