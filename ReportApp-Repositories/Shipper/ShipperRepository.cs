using Microsoft.EntityFrameworkCore;
using ReportApp_Models;

namespace ReportApp_Repositories.Shippers
{
    public class ShipperRepository : IShipperRepository
    {
        #region Injection
        private readonly IDbContext _context;
        public ShipperRepository(IDbContext context)
        {
            _context = context;
        }
        #endregion

        public IEnumerable<Shipper> GetAll()
        {
            return _context.Shippers;
        }

        public Shipper GetShipperById(int id)
        {
            return _context.Shippers.FirstOrDefault(x => x.ShipperID == id);
        }

        public void AddShipper(Shipper shipper)
        {
            _context.Shippers.Add(shipper);
            _context.SaveChanges();
        }

        public void UpdateShipper(Shipper shipper)
        {
            _context.SetEntityState(shipper, EntityState.Modified);
            _context.SaveChanges();
        }

        public void DeleteShipper(int id)
        {
            Shipper shipper = _context.Shippers.Find(id);
            _context.Shippers.Remove(shipper);
            _context.SaveChanges();
        }
    }
}
