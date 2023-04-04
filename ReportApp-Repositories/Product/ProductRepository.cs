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
            return _context.Products.ToList();
        }

        public IEnumerable<Product> GetProductsByCategory(int category)
        {
            return _context.Products
                .Where(x => x.CategoryID == category)
                .ToList();
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

    }
}
