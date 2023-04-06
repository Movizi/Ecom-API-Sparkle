using Microsoft.EntityFrameworkCore;
using ReportApp_Models;

namespace ReportApp_Repositories
{
    public interface IDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Supplier> Suppliers { get; set; }

        void SaveChanges();
        void SetEntityState<T>(T entity, EntityState state) where T : class;
    }
}
