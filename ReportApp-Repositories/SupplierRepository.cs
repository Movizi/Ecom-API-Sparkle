using Microsoft.EntityFrameworkCore;
using ReportApp_Contracts;
using ReportApp_Models;


namespace ReportApp_Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        #region Injection
        private readonly IDbContext _context;
        public SupplierRepository(IDbContext context)
        {
            _context = context;
        }
        #endregion

        public IEnumerable<Supplier> GetAll()
        {
            return _context.Suppliers;
        }

        public Supplier GetSupplierById(int id)
        {
            return _context.Suppliers.FirstOrDefault(x => x.SupplierID == id);
        }

        public void AddSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
        }

        public void UpdateSupplier(Supplier supplier)
        {
            _context.SetEntityState(supplier, EntityState.Modified);
            _context.SaveChanges();
        }
        public void DeleteSupplier(int id)
        {
            Supplier supplier = _context.Suppliers.Find(id);
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
        }

    }
}
