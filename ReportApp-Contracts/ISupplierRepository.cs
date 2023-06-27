using ReportApp_Models;

namespace ReportApp_Contracts
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetAll();
        Supplier GetSupplierById(int id);
        void AddSupplier(Supplier supplier);
        void UpdateSupplier(Supplier supplier);
        void DeleteSupplier(int id);
    }
}
