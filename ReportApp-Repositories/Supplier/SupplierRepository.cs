using Microsoft.EntityFrameworkCore;
using ReportApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportApp_Repositories.Suppliers
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
            return _context.Suppliers.ToList();
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
            var supplier = _context.Categories.Find(id);
            _context.Categories.Remove(supplier);
            _context.SaveChanges();
        }

    }
}
