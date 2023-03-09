using Microsoft.EntityFrameworkCore;
using ReportApp_Models;
using ReportApp_Repositories;

namespace ReportApp_API.Data
{
    public class AppDbContext : DbContext, IDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        // Implement IDbContext
        void IDbContext.SaveChanges()
        {
            base.SaveChanges();
        }
        public void SetEntityState<T>(T entity, EntityState state) where T : class
        {
            Entry(entity).State = state;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure your entity models here
        }
    }
}