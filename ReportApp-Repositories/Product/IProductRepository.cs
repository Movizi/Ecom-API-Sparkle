using ReportApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp_Repositories.Products
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(int category);
        void AddProduct(Product product);
        //Category GetCategoryById(int categoryId);
        //void AddCategory(Category category);
        //void UpdateCategory(Category category);
        //void DeleteCategory(int categoryId);
    }
}
