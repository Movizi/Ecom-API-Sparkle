using Microsoft.EntityFrameworkCore;
using ReportApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp_Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbContext _context;

        public ProductRepository(IDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .ToList();
        }

        public IEnumerable<Product> GetProductsByCategory(string categoryName)
        {
            return _context.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier)
                .Where(x => x.Category.CategoryName.Contains(categoryName))
                .ToList();
        }
    }
}
