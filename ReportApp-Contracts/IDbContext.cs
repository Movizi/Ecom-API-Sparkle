using Microsoft.EntityFrameworkCore;
using ReportApp_Models;

namespace ReportApp_Contracts
{
    public interface IDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Supplier> Suppliers { get; set; }
        DbSet<Shipper> Shippers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        void SaveChanges();
        void SetEntityState<T>(T entity, EntityState state) where T : class;
        string ConnectionString();
    }
}
